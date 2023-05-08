using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    /*WAY OF JUMPING USING A RIGIDBODY 
     
    //apply a forxct for the object to move 

    public Rigidbody rBody;
    public float jumpForce;
    public LayerMask groundMask;

    //Calling as colider which is the one that 
    public CapsuleCollider firstColider;

    //Space of allocation we are creating (shpere being thrown), also space has to be asigned to be able to be used
    //We'd only need to put one space because we don't need to know the results, only if is differet than 0

    public RaycastHit[] resultsWeNeed = new RaycastHit[20];

    //Finding out if is touching ground
    public bool isGrounded;

    //Creating a var required by physics function to assign to what distance will the ph be thrown
    public float groundCheckDistance;

    public float slope = 45f;

    public float gravityMultiplier = 1;

    public float fallForce = 0;

    // Update is called once per frame
    void Update()
    {
        //Down its applied for the force to only be applied in the frame 
        if (Input.GetButtonDown("Jump"))
        {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        var radious = firstColider.radius;
        var height = firstColider.height;
        var p1 = transform.position + Vector3.up * (height / 2);
        var p2 = transform.position - Vector3.up * (height / 2);


        //this creates inside of ints, a list of values for which we need to create our own space
        var collisionsAmount = Physics.CapsuleCastNonAlloc(p1, p2, radious, Vector3.down, resultsWeNeed, groundCheckDistance, groundMask);

        for (int i = 0; i < collisionsAmount; i++)
        {
            var currentCollision = resultsWeNeed[i];
            var currentNormal = currentCollision.normal;

            var angle = Vector3.Angle(Vector3.up, currentNormal);

            if(angle < slope)
            {
                isGrounded = true;
            }
        }

        if(rBody.velocity.y != 0)
        {
            rBody.AddForce(Vector3.down * gravityMultiplier);
            if(rBody.velocity.y < 0)
            {
                rBody.AddForce(Vector3.down * fallForce);
            }
        }


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rBody.constraints = RigidbodyConstraints.FreezeRotation;

        }

    }
    */

    public CharacterController characController;

    public bool isGrounded;

    public float gravityForce = 0;

    public float failForce;

    public float jumpHeight;

    public float currentVelocity;

    //ASK WHAT WAS THE RIGHT TYPE TO USE <CASTSOMETHING>
    public RaycastHit[] results = new RaycastHit[20];

    public LayerMask groudMask;

    void Update()
    {
        var radius = characController.radius;
        var halHeight = characController.height / 2f;
        var spherePivot = transform.position + Vector3.down * (halHeight - radius);
        var groundDistChecker = characController.skinWidth + 0.01f;

        var colisionAmt = Physics.SphereCastNonAlloc(spherePivot, radius, Vector3.down, results, groundDistChecker, groudMask);
        isGrounded = false;

        //Debug.Log(colisionAmt);
        for (int i = 0; i < colisionAmt; i++)
        {
            var currentCollision = results[i];
            var currentNormal = currentCollision.normal;
            var angle = Vector3.Angle(Vector3.up, currentNormal);
            isGrounded = angle < characController.slopeLimit;
            if (isGrounded) break;
        }
        //is -2 to assure the object touches the ground
        if (isGrounded)
        {
            currentVelocity = -2f;
        }
        else
        {
            //To sum gravity force
            currentVelocity -= gravityForce * Time.deltaTime;

            //The the speed is less than 0 then the object is falling 
            if (currentVelocity < 0)
            {
                currentVelocity -= failForce * Time.deltaTime;
            }
        }


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            currentVelocity = Mathf.Sqrt(jumpHeight * -2f * -gravityForce);
        }

        characController.Move(Vector3.up * currentVelocity * Time.deltaTime);

    }
}



