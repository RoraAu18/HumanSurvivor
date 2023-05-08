using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPool : MonoBehaviour
{
    Pool<TheParticle> particleSupply;
    TheParticle particle;
    
    // Start is called before the first frame update
    public void Start()
    {
        //Initializing the instance of the pool
        particleSupply.Innit();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            var newFireEffect = particleSupply.GetItem();
            

            //pending to activate the particle and move it as appropriate
        }
    }
}
