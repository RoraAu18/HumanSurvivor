using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pool responsibilities:
//1. Store Object Method, including lists to measure and control them
//2. Activate Object Method
//3. Recycle Method(?
public class ParticlePool<T> where T : Component
{
    public Transform dadParticle;
    public T childParticle;
    public int initialPoolSize;

    //Creating a list of objects to be able to access the objects in the pool
    public List<T> particlesInPool = new List<T>();

    //List to find out if is activated or not. 
    public List<bool> isTheParticleActivated = new List<bool>();
    public void ParticlesInitialization()
    {
        //Activating Method which creates and stores the items in the pool 
        AddMoreParticles(initialPoolSize);
    }

    //1. We must add the objects to the pool meant to store them
    public void AddMoreParticles(int addBy)
    {
        for (int i = 0; i < addBy; i++)
        {
            //initializing the object
            var newBornParticle = GameObject.Instantiate(childParticle, dadParticle);
            particlesInPool.Add(newBornParticle);
            //No need to activate them yet in the list
            isTheParticleActivated.Add(false);
            newBornParticle.gameObject.SetActive(false);
        }
    }

    //2. Method to activate particles when necesary 
    public T ActivatingParticlesToFire()
    {
        for (int i = 0; i < particlesInPool.Count; i++)
        {
            if(isTheParticleActivated[i] == false)
            {
                isTheParticleActivated[i] = true;
                return particlesInPool[i];
            }
        }
        var ifRunningOutOfParticles = particlesInPool.Count;
        AddMoreParticles(5);
        return particlesInPool[ifRunningOutOfParticles];
    }
    //3. Recycling
    public void ParticleRecyclingSystem(T particle)
    {
        var accessToIndex = particlesInPool.IndexOf(particle);
        //access to the item T in the list and deactivate the object
        isTheParticleActivated[accessToIndex] = false;
    }
}
