using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedMovement : MonoBehaviour
{
    public float currentVelocity = 0.3f;
    public float acceleration = 0;
    public float maxVelocity = 1.0f;


    // Update is called once per frame
    void Update()
    {
        //Creating keyboard imputs
        float MoveZ = Input.GetAxisRaw("Vertical");
        float MoveX = Input.GetAxisRaw("Horizontal");
        Debug.Log(MoveX + " " + MoveX);
        //Creating Vector from the unity reserved space
        Vector3 movingVector = Vector3.zero;

        //Asigning imputs to 
        movingVector.z = MoveZ;
        movingVector.x = MoveX;
        movingVector = movingVector.normalized;


        //MovingVector are just direction 

        if (movingVector.magnitude != 0)
        {
            currentVelocity += acceleration * Time.deltaTime;
            if (currentVelocity > maxVelocity)
            {
                currentVelocity = maxVelocity;
            }

            transform.position += movingVector * currentVelocity * Time.deltaTime;

        }
        else
        {
            currentVelocity = 0;
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            currentVelocity -= acceleration * Time.deltaTime;
        }
    }
}
