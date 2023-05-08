using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCameraCC : MonoBehaviour
{
    private Vector3 originalPosition;
    public float speed = 100;
    public Vector2 minLimiti;
    public Vector2 maxlLimit;
    private void Start()
    {
        originalPosition = transform.position;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
 
        //Debug.Log(mouseX+ " "+ mouseY);


        if (Input.GetButton("Fire1"))
        {
            //We use the direction vector to move the object twoards itself instead of towards the world
            var newPos = transform.position + transform.up * -mouseY * speed * Time.deltaTime;
            newPos += transform.right * -mouseX * speed * Time.deltaTime;

            var rightMov = newPos - originalPosition;
            var magRight = rightMov.magnitude;

            //Calculating the amount of angles rotated from one posistion to the other (the right Mov)
            //We use "-" because naturally, in the pc, it'll substract to the right and add to the left we need the other way around
            var horizontalDirection = -Vector3.SignedAngle(originalPosition, rightMov, transform.up);

            if(magRight > Mathf.Abs(minLimiti.x) && horizontalDirection <0) 
            {
                var offsetMag = magRight - Mathf.Abs(minLimiti.x);
                var offsetDir = transform.right;
                var fixedVector = offsetDir * offsetMag;
                newPos += fixedVector;
            }
            else if(magRight > maxlLimit.x && horizontalDirection > 0)
            {
                var offsetMagnitude = magRight - maxlLimit.x;
                var offsetDir = -transform.right;
                var fixedVector = offsetDir * offsetMagnitude;
                newPos += fixedVector;
            }

            var upMov = newPos - originalPosition;
            upMov = Vector3.Project(upMov, transform.up);
            var magnitudeUp = upMov.magnitude;
            var verticalDir = Vector3.SignedAngle(originalPosition, upMov, transform.right);
            if(magnitudeUp > Mathf.Abs(minLimiti.y) && verticalDir < 0)
            {
                var offSetMag = magnitudeUp - Mathf.Abs(minLimiti.y);
                var offsetDir = transform.up;
                var fixedVector = offsetDir * offSetMag;
                newPos += fixedVector;
            }
            else if(magnitudeUp > maxlLimit.y && verticalDir > 0)
            {
                var offSetMagnitude = magnitudeUp - maxlLimit.y;
                var offsetDirecton = -transform.up;
                var fixedVector = offsetDirecton * offSetMagnitude;
                newPos += fixedVector;
            }
            transform.position = newPos;
        }


    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        var limitUpLeft = originalPosition + transform.right * minLimiti.x;
        limitUpLeft += transform.up * maxlLimit.y;
        /*
        var limitUpRight = originalPosition + transform.up * minLimiti.x;
        limitUpRight -= transform.right * maxlLimit.y;

        var limitDownLeft = originalPosition + transform.right * minLimiti.y;
        limitDownLeft -= transform.up * maxlLimit.x;
        */
        var limitDownRight = originalPosition + transform.up * minLimiti.y;
        limitDownRight += transform.right * maxlLimit.x;

        Gizmos.DrawLine(limitUpLeft, limitDownRight);
        //Gizmos.DrawLine(limitDownRight, limitUpLeft);
        //Gizmos.DrawLine(limitUpRight, limitUpLeft);
    }
}
