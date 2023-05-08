using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    public CharacterController Character1;

    public float acceleration = 5;
    public float currSpeed = 0;
    public float maxSpeed = 5;

    void Start()
    {
        
        //"out" means that we are returning a value, i'll try to match the type of controller where you have it as soon as you press play.
        //This is to set the Game Object to the behavior automatically.
        if(Character1 == null)
        {
            TryGetComponent(out Character1);
        }

    }

    // Update is called once per frame.
    void Update()
    {
        // Creating the Input vars with the GetAxisFunction and quoting the predeterminated or default in the input manager.
        var xMov = Input.GetAxisRaw("Horizontal");
        var zMov = Input.GetAxisRaw("Vertical");

        //Creating the vector because that´s what you use to graphic and control movement here, is three because is 3Dimentional and "zero" because starts in 0,0,0 cordinates. 
        var vectorThatMoves = Vector3.zero;

        //Asignig the relative line where each localvar with the key input will move towards.
        vectorThatMoves.x = xMov;
        vectorThatMoves.z = zMov;

        //*vectorThatMoves = vectorThatMoves.normalized;
        vectorThatMoves.Normalize();

        //Accelerate by multiplying the current speed.
        currSpeed += acceleration * Time.deltaTime;

        //Clamp (Sujetar) Returns an Int within the given values and the frame given.  
        currSpeed = Mathf.Clamp(currSpeed, -maxSpeed, maxSpeed);

        //Applying the math to the vector that moves (you gotta always multiply as only that makes sense).
        var speedVector = vectorThatMoves * currSpeed * Time.deltaTime;

        //magnitude is the lenght of the vector- To restart velocity
        if (speedVector.magnitude == 0)
        {
            currSpeed = 0;
        }

        // ToTheCharacter.ApplyMovementToTheGameObject.FromTheCreatedVector.
        Character1.Move(-speedVector);
    }
}
