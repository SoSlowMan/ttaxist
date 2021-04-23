using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody theRB;
    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 250f, gravityForce = 10f, dragOnGround = 3f;
    public float speedInput, turnInput;
    public bool grounded;
    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;
    public Transform frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;
    public float maxWheelTurn = 25;
    public float rotationSpeed = 0.1f, wheelRotationSpeed = 2f;
    public Quaternion zeroX;

    // Start is called before the first frame update
    void Start()
    {
        theRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        } 
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");
        //мб сделать поворот только на земле
        if (theRB.velocity.magnitude > 2 && grounded == true)
        {
            if (speedInput >= 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput / 100f * turnStrength, 0f));
            }
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0f, turnInput / 100f * turnStrength, 0f));
            } 
        }       
        //wheel animation
        frontLeftWheel.localRotation = Quaternion.Euler(frontLeftWheel.localRotation.eulerAngles.x + (speedInput), turnInput * maxWheelTurn, 0);
        frontRightWheel.localRotation = Quaternion.Euler(frontRightWheel.localRotation.eulerAngles.x + (speedInput), turnInput * maxWheelTurn, 0);
        backLeftWheel.localRotation = Quaternion.Euler(backLeftWheel.localRotation.eulerAngles.x + (speedInput), 0, 0);
        backRightWheel.localRotation = Quaternion.Euler(speedInput * maxWheelTurn, 0, 0);


        transform.position = theRB.transform.position;

    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if(grounded)
        {
            theRB.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                theRB.AddForce(transform.forward * speedInput);
            }
        } else
        {
            theRB.drag = 0.1f;
            theRB.AddForce(Vector3.up * -gravityForce * 100);
            zeroX = Quaternion.Euler(10, transform.rotation.y, transform.rotation.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, zeroX, rotationSpeed);
        }

       
    }
}
