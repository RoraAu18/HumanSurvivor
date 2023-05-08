using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCAM : MonoBehaviour
{
    public Transform playerBody;
    public float speed = 10;
    public float ylimit = 33;
    public float yAngle = 0;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        playerBody.transform.eulerAngles += Vector3.up * mouseX * speed * Time.deltaTime;

        var angles = transform.localEulerAngles;

        yAngle += mouseY * speed * Time.deltaTime;
        yAngle = Mathf.Clamp(yAngle, -ylimit, ylimit);

        angles.x = yAngle;
        transform.localEulerAngles = angles;

    }
}
