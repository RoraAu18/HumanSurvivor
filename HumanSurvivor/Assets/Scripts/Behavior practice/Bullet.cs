using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    //public Rigidbody bulletRB; No need to us a RB as we don't need the physiscs for now
    //Adding velocity force to the shooting

    Pool<Bullet> reusingPool;
    Pool<Recycling> reusingParticle;
    CharacterController owner;
    public AttackBehavior attackBehavior;
    public float useTime = 5;
    public float countTime = 0;
    public float damage = 0;

    //This is how you bring an instance of the script into another one: Name of the script and ref var name.
    public ColisionController CController;
    public float shootingForce = 250;

    public void Init(Pool<Bullet> _reusingPool, CharacterController _owner, Pool<Recycling> _reusingParticle)
    {
        //send me to a pool
        reusingPool = _reusingPool;
        owner = _owner;
        reusingParticle = _reusingParticle;
        //Always use TryGetComponent to bring the instance of the script instead of GetComponet as it uses less resources
        TryGetComponent(out CController);
        TryGetComponent(out attackBehavior);
        //TryGetComponent<ColisionController>(out CController);
        //Hereby we should subscribe the Actions in case we need to
        Debug.Log("se esta llamando el collider?");
        CController.collisionEnter += HandlingCollider;
        //Debug.Log(CController.collisionEnter + "holi");
        countTime = 0;
    }
        
    //public Pool<RecyclingBullets> reusingPool;
    // Update is called once per frame
    void Update()
    {
        //The default position needed is the forward of the parent, therefore we can use .fw.
        //Calling position of the object, it moves towards "+=" the forward of the object he's in at some force at delta time to normali<ze frames

        transform.position += transform.forward * shootingForce * Time.deltaTime;
        



        countTime += Time.deltaTime;
        if (countTime >= useTime)
        {
            RecyclinABullet();
        }


        //Trying to get the pool to recycke without changing the type of the prev pool 
        

        /*
        if (whimTarget.magnitude != 0)
        {
            velocity += shootingForce * Time.deltaTime;
            transform.position += whimTarget * velocity * Time.deltaTime;
        }
        */
        //STILL HAVE TO CONNECT THE BULLET MOVEMENT TO THE OBJECTS CREATED I N THE POOL

        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            bullet.AddForce(transform.forward * 100);
            velocity += shootingForce * Time.deltaTime;
            bullet.transform.position += transform.forward * shootingForce * Time.deltaTime;
            //transform.position += transform.forward * shootingForce * Time.deltaTime;
        }
        */

        //Instantiate a pool of particles for the effect
    }
    
    //Create a generic method to handle the collisions in the bullet
    public void HandlingCollider(Collider objectCollider)
    {
      
        //This is a reference for the the object that is colliding, we want to find out if is the owner of the bullet
        CharacterController collidedCC;
        if (objectCollider.TryGetComponent(out collidedCC))
        {
            if (collidedCC == owner)
            {
                //Play the particle when colliding with anything
                return;
            }
        }

        
        //Here is where we will give the bullet the capacity to provide damage
        //If the collider we collides with, has a damage controller, store it in the var and give damage to the object 
        if(objectCollider.TryGetComponent(out DamageController damageReceiver))
        {
            attackBehavior.Attack(damageReceiver);
        }

        RecyclinABullet();
        var newParticle = reusingParticle.GetItem();
        newParticle.Innit(reusingParticle);
        //give the particle the position of the bullet
        newParticle.transform.position = transform.position;
        newParticle.gameObject.SetActive(true);


    }

    //We create an independent method to be more efficient when calling in different scenarios
    public void RecyclinABullet()
    {
        //From the ppol, call the method to return the obj back to the pool and this menas this bullet 
        reusingPool.RecycleItem(this);
        gameObject.SetActive(false);
        Debug.Log("Reciclé la bala");
        //We unsubscribe the action in the instance of the o0bject every time the object gets recycled to use less resources and to sort of reset the action.
        CController.collisionEnter -= HandlingCollider;
    }
}
