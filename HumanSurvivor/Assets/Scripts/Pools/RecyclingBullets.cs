using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingBullets : MonoBehaviour
{
    //Quote the loadingAGun pool and asign a new type of pool
    Pool<RecyclingBullets> reusingPool;

    public float useTime = 5;
    public float countTime = 0; 

    public void Initialization(Pool<RecyclingBullets> _prefabBulletPool)
    {
        reusingPool = _prefabBulletPool;
    }

    private void Update()
    {
        countTime += Time.deltaTime;

        if (countTime >= useTime)
        {
            //From pool call method from the bullet's pool and (this) is used to reference the object in use
            reusingPool.RecycleItem(this);
            //Deactivating object
            gameObject.SetActive(false);
            countTime = 0;
        }
    }
}
