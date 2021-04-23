using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 playerPos, cameraPos;
    public Transform Player;
    public Transform lookTarget;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    public float rotationSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //playerPos = new Vector3(Player.transform.position.x, Player.transform.position.y + 3.15f, Player.transform.position.z - 6f);
        //cameraPos = playerPos;
        //Vector3 desiredPos = Player.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        //transform.position = smoothedPosition;
        //transform.rotation = Quaternion.Euler(Player.transform.rotation.eulerAngles.x + 15, Player.transform.rotation.eulerAngles.y, Player.transform.rotation.eulerAngles.z);
        //transform.LookAt(lookTarget);

        var targetPosition = Player.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        var direction = Player.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
    }
}
