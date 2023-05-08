using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageCounter = 100;


    // Start is called before the first frame update
    void Init()
    {
        //cControl.collisionEnter += ManagingCollider;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ManagingCollider(Collider collider)
    {
        damageCounter = -25;

    }
}
