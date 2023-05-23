using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionController : MonoBehaviour
{
    //Easy but not good way to detect a collision (not good for the memory)
    //It is better to not add this type of thing to each object but to create a single collition detection for every object
    /*
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I hit something");
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("I hit something");
    }
    */

    //referencing lists to use later (are not lists until they are assinged)
    public List<Collider> currentFrameCollissions = new List<Collider>();
    public List<Collider> lastFrameCollissions = new List<Collider>();
    //Arraly to store
    Collider[] collisions = new Collider[30];
    public Collider theCollider;
   
    public bool foundOnCurrentCollision = false;
    public LayerMask layerMask;
    public int amountOfObjectsHit = 0;
    

    //Actions are in the using System import, we make them public.
    //Actions are call-to-execute-method-with-no-instance-required operators 
    //They can have many methods subscribed from different parts of the code, we shall always use "?" sysmbol when calling it so in case they don´t have any, we won´t get an error
    //We must especify with <> the fact that the Action receives a type of object to be able to identify it later on when necessary.
    public Action<Collider> collisionStay;
    public Action<Collider> collisionEnter;
    public Action<Collider> collisionExit;
    

    //This could be the Awake function too
    //We set the instance of the object we  created
    public void Start()
    {
        //TryGetComponent(out theCollider);
    }

    public void Update()
    {
        //making the lastFC a copy of curentFC
        lastFrameCollissions = currentFrameCollissions.GetRange(0, currentFrameCollissions.Count);
        //Clearing the list
        currentFrameCollissions.Clear();
        //Non alloc means that unity is not saving space in  the memory

        int objsHittedAmount = 0;
       
        //Debug.Log("Estoy guardandome" + gameObject.name + " " + amountOfObjectsHit);

        if (theCollider is CapsuleCollider myCapsuleCollider)
        {
            objsHittedAmount = Physics.OverlapSphereNonAlloc(transform.position, myCapsuleCollider.radius, collisions, layerMask);
        }

        if (theCollider is BoxCollider myBoxCollider)
        {
            objsHittedAmount = Physics.OverlapBoxNonAlloc(transform.position, myBoxCollider.size / 2, collisions, transform.rotation, layerMask);
        }
        else if (theCollider is CharacterController myCharacterCollider)
        {
            var center = myCharacterCollider.transform.position;
            var point1 = (myCharacterCollider.height / 2) * Vector3.up + center;
            var point0 = (myCharacterCollider.height / 2) * Vector3.down + center;

            objsHittedAmount = Physics.OverlapCapsuleNonAlloc(point0, point1, myCharacterCollider.radius, collisions, layerMask);
        }
        else if (theCollider is SphereCollider sphereCollider)
        {
            var pos = sphereCollider.transform.position;
            var rad = sphereCollider.radius;

            objsHittedAmount = Physics.OverlapSphereNonAlloc(pos, rad, collisions, layerMask);
        }


        for (int i= 0; i< objsHittedAmount; i++)
        {
            var collidedObject = collisions[i];
            if (collidedObject == theCollider) continue;
            currentFrameCollissions.Add(collidedObject);
            var wasCollidingAlready = lastFrameCollissions.Contains(collidedObject);
            if (!wasCollidingAlready)
            {
                //executing the action of invoking?, note that we also especify the type of object as required per actiona and generic colider handling function
               
                //Debug.Log("Collision enter" + collidedObject+ " ", this);
                collisionEnter?.Invoke(collidedObject);
                
            }
        }

        for (int i = 0; i < lastFrameCollissions.Count; i++)
        {
            var lastCollision = lastFrameCollissions[i];
            bool foundOnCurrentCollision = false;

            for (int j= 0; j < currentFrameCollissions.Count; j++)
            {
                var currentCollilsion = currentFrameCollissions[j];
                if(currentCollilsion == lastCollision)
                {
                    foundOnCurrentCollision = true;
                    break;
                }
            }
            if (foundOnCurrentCollision)
            {
                //We invoke the lastcollision as it represents the element in the lsit where we were able to measure the status of the collision
                collisionStay?.Invoke(lastCollision);
                //Debug.Log("Collision stay");
            }
            else
            {
                //Same as prev note 
                collisionExit?.Invoke(lastCollision);
                //Debug.Log("Collision Exit");
            }
        }
    } 
}
