using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    //This establish the type of the T
    public Pool<Recycling> cubesPool;


    public void Start()
    {
        cubesPool.Innit();
    }

    //Add controler with mouse left click gwt an item from the pool, add it as a gameObject and activate it (DO)
    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            var newItem = cubesPool.GetItem();
            newItem.Innit(cubesPool);
            newItem.transform.position = cubesPool.parent.position;
            newItem.TryGetComponent(out Rigidbody newItemRigidbody);

            newItemRigidbody.velocity = Vector3.zero;
            newItemRigidbody.angularVelocity = Vector3.zero;

            newItem.gameObject.SetActive(true);

        }
    }

    //TASK:instance pool to create a bullet with a constan movement as sson as it activates 
}
