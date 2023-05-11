using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public CharacterController playerCC;
    

    private RaycastHit[] results = new RaycastHit[12];
    public LayerMask myLayerGroundMask;
    public bool isGrounded;
    private bool oldIsGrounded;
    public float fallforce;
    public float gravityForce;
    public float jumpHeight;
    private float currYVelocity;


    void Start()
    {
        oldIsGrounded = true;
    }

    void Update()
    {
        var direction = transform.up;
        var offset = playerCC.height / 2 - playerCC.radius;
        var localPoint0 = playerCC.transform.position - direction * offset;
        var localPoint1 = playerCC.transform.position + direction * offset;
        var radius = playerCC.radius;
        var groundCheckDist = playerCC.skinWidth + 0.01f;

        var collisionAmount = Physics.CapsuleCastNonAlloc(localPoint0, localPoint1, radius, Vector3.down, results, groundCheckDist, myLayerGroundMask);

        isGrounded = false;


        for (int i = 0; i < collisionAmount; i++)
        {
            var currCollision = results[i];
            var currNormal = currCollision.normal;
            var angle = Vector3.Angle(Vector3.up, currNormal);
            isGrounded = angle < playerCC.slopeLimit;
            if (isGrounded) break;
        }
        if (isGrounded)
        {
            currYVelocity = -2;
        }
        else
        {
            if (currYVelocity > 0)
            {
                currYVelocity -= fallforce * Time.deltaTime;
            }
            currYVelocity -= gravityForce * Time.deltaTime;
        }
        if (isGrounded && Input.GetButton("Jump"))
            
        {
            currYVelocity = Mathf.Sqrt(jumpHeight * 2f * gravityForce);
            isGrounded = false;
        }

        playerCC.Move(Vector3.up * currYVelocity * Time.deltaTime);
        oldIsGrounded = isGrounded;
    }
}