using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheParticle : MonoBehaviour
{
    Pool<TheParticle> poolOfParticles;
   
    public ParticleSystem parSysm;
    //public Bullet balita;
    //bring here the colliding bullet and act in accordance to its collider EXIT 

    // Start is called before the first frame update
    public void Commance(Pool<TheParticle>_particlePool)
    {
        //Activating the instance of the pool we created
        poolOfParticles = _particlePool;
        TryGetComponent(out parSysm);
        //parSysm.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Turn off looping 
        //You can reference structures
        /*
        var particleControl = parSysm.main;
        particleControl.startLifetime = 1f;
        */

    }


    /*
    public void HandlingCollider(Collider actWhenColliding)
    {
        if (whenColliding)
        {
            parSysm.Play();
            parSysm.Stop();
        }
        
    }*/
}
