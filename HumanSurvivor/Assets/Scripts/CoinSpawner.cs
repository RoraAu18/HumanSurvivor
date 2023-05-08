using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour, iSpawnerUsers<Collectable>
{
    public Spawner<Collectable> spawner;

    public void Awake()
    {
        spawner.Init(this);
    }
    // Update is called once per frame
    void Update()
    {
        spawner.Tick();
    }

    public void OnSpawnedCustomizable(Collectable newItem, Pool<Collectable> pool)
    {
        newItem.Init(pool);
    }
}
