using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Initialize, Activating, Reciclying
public class BulletSpawner : MonoBehaviour
{
    //When calling the created pool we will assign the type of object we want the pool to be of.
    public Pool<Bullet> BulletSupply;
    public Pool<Recycling> _reusingParticle;
    //public Pool<RecyclingBullets> reusingPool;
    public float useTime = 5;
    public float countTime = 0;
    public CharacterController character;

    // Start is called before the first frame update
    public void Start()
    {
        BulletSupply.Innit();
        TryGetComponent(out character);
    }
    /*
     * 
    public void RecyclingInit(Pool<RecyclingBullets> _recyclingPool)
    {
        reusingPool = _recyclingPool;
    }
    */

    // Update is called once per frame
    public void Update()
    {
        //"Down" because it'll only work when pressing the button for the first time and not while pressing it (As in GetButton)
        if (Input.GetButtonDown("Fire2")) 
        {   
            //Creating the var were the new bullet gets stored.
            var newBullet = BulletSupply.GetItem();

            //Sending the reference to the pool, it wasn´t done
            newBullet.Init(BulletSupply, character, _reusingParticle);
            //Activating the game object (with Lowkey as with cap would be a component).
            newBullet.gameObject.SetActive(true);

            Debug.Log(newBullet.gameObject.activeInHierarchy);
            //Making sure the new bullet's forward were it'll move toward to, is the same as the one of the game Object the script is in.
            newBullet.transform.forward = transform.forward;
            //Making sure the position were the new bullet appears is the same as the position of the parent object.
            newBullet.transform.position = transform.position;



            /*
            This code would only work in one frame so the time is not stored
            countTime += Time.deltaTime;

            if (countTime >= useTime)
            {
                newBullet.gameObject.SetActive(false);
                countTime = 0;
            }*/
  
        }

        //Recycling
    }
    
    /*public void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(perspectiveCamera.transform.position, perspectiveCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        } 
    }
    */
}
