using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Add this
using System;


//If a class is marked as this, unity saves the changes we made as metadata so the values you place in the inspector will be stored 
[Serializable]

//<> This means that the pool can only store components (but not behaviors)
public class Prefabpool<T> where T : Component
{
    // We will transform all childs in the folder object 
    public Transform parent;

    //Gives to the prefab the type of the original type of component (T)
    public T originalPrefab;
    public int initialSize;

    public List<T> poolItems = new List<T>();
    public List<bool> activeItems = new List<bool>();


    public void Innit()
    {
        GrowPool(initialSize);
    }

    public void GrowPool(int amount)
    {
        //Increasing the amount of objets in the pool (normally do when initiating the game so the player doesn't notice)
        for (int i = 0; i < amount; i++)
        {
            var newItem = GameObject.Instantiate(originalPrefab, parent);
            poolItems.Add(newItem);
            activeItems.Add(false);
            newItem.gameObject.SetActive(false);
        }
    }

    // in the generic type T we created apply method get a pool item for each pool item needed
    public T GetPoolItem()
    {
        for (int i = 0; i < poolItems.Count; i++)
        {
            //if items aren´t activated the activate them  
            if (activeItems[i] == false)
            {
                activeItems[i] = true;
                return poolItems[i];
            }

        }
        //This part will be exe ted only if the previous return doesn´t work for ovious reasons
        //This variable stores the last item count so that when the method restarts afterwards it starts lookming from the new created items
        var lastAddesObject = poolItems.Count;
        //Execute the growpool for certain amt
        GrowPool(5);
        activeItems[lastAddesObject] = true;
        //Execute GetPoolItem 
        //TO ASK: i missed where the prev var gets used
        return poolItems[lastAddesObject]; 
    }
    public void RecycleItem(T item)
    {
        var idx = poolItems.IndexOf(item);
        activeItems[idx] = false;
    }


}
