using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour, iSpawnerUsers<AIEnemyController>
{
    public Spawner<AIEnemyController> spawner;

    public void Awake()
    {
        spawner.Init(this); 
        
    }

    public void Update()
    {
        spawner.Tick();
    }
    public void OnSpawnedCustomizable(AIEnemyController newItem, Pool<AIEnemyController> pool)
    {
        newItem.Init(pool);
    }



    /*
    public Pool<AIEnemyController> poolOfEnemies;

    public float countTime = 0;
    public float maxTime = 1;
    public float spawnRange = 0;


    public void Start()
    {
        countTime = maxTime;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if(countTime >= maxTime)
        {
            var newEnemy = poolOfEnemies.GetItem();

            Vector3 randomPosition = transform.position;
            randomPosition.x += Random.Range(-spawnRange, spawnRange);
            randomPosition.z += Random.Range(-spawnRange, spawnRange);

            newEnemy.Init(randomPosition, poolOfEnemies);
            countTime = 0;
        }
        

    }
    */
}
