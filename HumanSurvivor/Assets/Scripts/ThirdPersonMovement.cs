using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController cController;
    //What we aim to transform
    public Transform cam;

    public float speed = 1f;
    public float smoothingRotationBy = 0.1f;

    //Ref type of object (Not explained what for but necessary)
    private float rotVelocity;

    // Update is called once per frame
    void Update()
    {
        //Whatever key input assigned in the Hor and Ver will be stored in the variables
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //creating a vector
        Vector3 dir = Vector3.zero;

        //assigning the values from the vars to the Vector default directions and normalizing to 1 each time. 
        dir.z = vertical;
        dir.x = horizontal;
        dir = dir.normalized;

        //if the object{s magnitude (or movement) is different than 0, then:
        if(dir.magnitude >= 0.1f) 
        {
            //assign the Target angle to the product of the calculated angle in Degs plus the rotation face the camara has
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float currentAngle = transform.eulerAngles.y;

            float smoothedAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref rotVelocity, smoothingRotationBy);

            Vector3 angleForLooking = Vector3.up * smoothedAngle;

            transform.eulerAngles = angleForLooking;

            Vector3 angleForMovement = Vector3.up * targetAngle;
            var movDir = Quaternion.Euler(angleForMovement) * Vector3.forward;

            cController.Move(movDir.normalized * speed * Time.deltaTime);

            

        }
    }
}
