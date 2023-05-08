using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCCMovement : MonoBehaviour
{
    public Transform player;

    public float yRotLimit;
    public float rotSpeed = 1;

    public float movingSmootherBy = 0.2f;

    private Vector3 refSpeed = Vector3.zero;

    public float yAngle = 0;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        /*
        transform.eulerAngles += Vector3.up * mouseX * rotSpeed * Time.deltaTime;

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref refSpeed, movingSmootherBy);

        var targetRotInX = transform.eulerAngles - Vector3.right * (mouseY * rotSpeed * Time.deltaTime);
        
        if(targetRotInX.x < 0)
        {
            targetRotInX.x = 360 - targetRotInX.x;
        }

        if(targetRotInX.x > yRotLimit && targetRotInX.x <= 180)
        {
            targetRotInX.x = yRotLimit;
        } else if (targetRotInX.x < 360 - yRotLimit && targetRotInX.x >= 180)
        {
            targetRotInX.x = 360 - yRotLimit;
        }

        transform.eulerAngles = targetRotInX;
        */
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        transform.eulerAngles += Vector3.up * mouseX * rotSpeed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref refSpeed, movingSmootherBy);

        var currAngles = transform.eulerAngles;

        yAngle += mouseY * rotSpeed * Time.deltaTime;
        yAngle = Mathf.Clamp(yAngle, -33, 33);
        currAngles.x = yAngle;
        transform.eulerAngles = currAngles;
    }
}
