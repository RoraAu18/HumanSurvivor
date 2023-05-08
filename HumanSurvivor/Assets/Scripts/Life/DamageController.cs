using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iLifeController
{
    public void OnDamage();
    public void OnDeath();
}

//This code is just to figure and get the damage, the conditions of that damage should remain generic so we can reuse it and assign it later
//For the behavior we will call it as a "behavior" in the name
//If the code affects the state of the game object, that's a controller

public class DamageController : MonoBehaviour
{
    public float damageCounter = 100;
    public float inicialLive = 100;
    public bool isDead = false;

    //Set up the list of classes with the interface so that we can modify their attributes later on
    public iLifeController[] users;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    //At first, this code will only reset the initcial life and set the ovject as alive
    public void Init()
    {
        //Making sure we restart the code
        inicialLive = damageCounter;
        isDead = false;
        //Store the users of the interface in a list (here we can use getComponents)
        users = GetComponents<iLifeController>();
        //Find if the objects are being stored
        for (int i = 0; i < users.Length; i ++ )
        {
            Debug.Log(users[i]);
        }
    }

    public void GetDamage(float damageAmount)
    {
        for (int i = 0; i < users.Length; i ++ )
        {
            //calling each get Damage method in each object each time the object received damage 
            users[i].OnDamage();
        }
        //reduce the counter in each hit
        damageCounter -= damageAmount;
        //IF dead, call each ondeath hmethod 
        if (isDead == false && damageCounter <= 0)
        {
            isDead = true;
            for (int i = 0; i < users.Length; i++)
            {
                users[i].OnDeath();
            }
            damageCounter = 0;
        }
    }
}
