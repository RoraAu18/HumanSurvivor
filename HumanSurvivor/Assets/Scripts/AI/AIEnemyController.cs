using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyController : MonoBehaviour, iLifeController
{
    public SMNode mainNode;
    public DamageController damageReceiver;
    public SMContext aiContext;
    Pool<AIEnemyController> recycling;
    public float lifeSpan = 5;
    public float timer = 0;
    public Rigidbody rbody;

    public int pointsOnHit = 5;
    public int deathsCount = 1;


    public void Init(Pool<AIEnemyController> _recyclingEnemy)
    {
        //transform.position = position;
        recycling = _recyclingEnemy;
        gameObject.SetActive(true);
        TryGetComponent(out damageReceiver);
        timer = 0;
        //The enemy has to know which is his Rbdy and the following lines are the rbody reinitializing
        /*
        TryGetComponent(out rbody);
        rbody.ResetInertiaTensor();
        rbody.velocity = Vector3.zero;
        rbody.angularVelocity = Vector3.zero;
        */
    }
    void Update()
    {
        mainNode.Run(aiContext);
        timer += Time.deltaTime;
        if (timer >= lifeSpan)
        {
            RecyclingEnemy();
        }
    }

    public void OnDamage()
    {
        //letting the game manager know the enemy got hit and activating the points deduction in the GUI
        GameManager.OnlyInstance.AddScore(pointsOnHit);
        Debug.Log("entrando al enemy OnDamage");
        if (damageReceiver.damageCounter <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        if (damageReceiver.isDead)
        {
            RecyclingEnemy();

        }
        //letting the game manager know the enemy died and activating the deaths counts in the GUI
        GameManager.OnlyInstance.AddScore(pointsOnHit);

    }
    public void RecyclingEnemy()
    {
        //informing the pool that the item is ready to reuse.
        recycling.RecycleItem(this);
        gameObject.SetActive(false);
    }

}
