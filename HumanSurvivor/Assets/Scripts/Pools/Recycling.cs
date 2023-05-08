using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycling : MonoBehaviour
{
    //Instancing the type of pool
    Pool<Recycling> pool;

    public float lifeTime = 5;

    public float timer = 0;

    public void Innit(Pool<Recycling> _prefabpool)
    {
        pool = _prefabpool;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= lifeTime)
        {
            pool.RecycleItem(this);
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}
