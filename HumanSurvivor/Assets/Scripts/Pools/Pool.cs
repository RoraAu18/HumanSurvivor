using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//POOL: Has the responsibility to store everything
//To visualize the pool in the inspector
[Serializable]

//Creating a pool with a general type of object "T" which will later-on be assigned in the spawnable script
public class Pool<T> where T : Component
{
    //parent where all the objects are created and it has to be seen in the scene
    //Place in the scene and the inspector where we leave the instances so is less messy.
    public Transform parent;
    //original prefab object
    public T originalBullet;
    //amt of bullets when activated at first
    public int initSize;
    //Creating a list

    public List<T> itemsInPool = new List<T>();


    //List for each item to find out if the item is in use or activated.    
    public List<bool> activated = new List<bool>();

    // Start is called before the first frame update
    public void Innit()
    {
        IncreaseingPool(initSize);
    }

    // Update is called once per frame
    //Method to create Bullets out of the parent and increase the pool if necessary "BY" to futurely assign 
    public void IncreaseingPool(int growBy)
    {
        //(set the value of the i; action for the is; Add)
        for (int i = 0; i < growBy; i++)
        {
            //the new bullet will be instatiated from the original bullet and the parent
            var newBullet = GameObject.Instantiate(originalBullet, parent);
            //Add the new object to the pool
            itemsInPool.Add(newBullet);
            //the new bullet in the list won't be automatically activated
            activated.Add(false);
            //gameobject defines whether or not he object is activated, methods receive bools.
            //the game object of the bullet sets active
            newBullet.gameObject.SetActive(false);
        }
    }


    //To load the gun method, activates the objects
    public T GetItem()
    {
        for (int i = 0; i < itemsInPool.Count; i++)
        {
            if (activated[i] == false)
            {
                activated[i] = true;
                //Note that if this return works, the next lines of code won't go through
                return itemsInPool[i];
            }
        }
        //Add to the list new items without overwritting them if there´s no bullet to use, this after the initian size is actiivated
        var lastItemAdded = itemsInPool.Count;
        IncreaseingPool(5);
        activated[lastItemAdded] = true;
        return itemsInPool[lastItemAdded];
    }

    //Creating the recycle method
    //Getting the index an object from the pool and deactivated
    public void RecycleItem(T bull)
    {
        var index = itemsInPool.IndexOf(bull);
        activated[index] = false;
    }
}


