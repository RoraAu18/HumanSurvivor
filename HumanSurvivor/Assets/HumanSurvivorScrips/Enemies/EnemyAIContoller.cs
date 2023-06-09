using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyAIContoller : MonoBehaviour
{
    public SMNode mainNode;
    public bool gotDistraction;
    //[HideInInspector]
    public bool chasingPlayer;
    [SerializeField]
    NavMeshAgent aiAgent;
    [SerializeField]
    SMContext context;
    [SerializeField]
    EnemyStates enemyStates;
    EnemyStates currentAnimsState;
    Rigidbody rb;
    public Action<EnemyStates> onEnemyStateChange;
    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out aiAgent);
        mainNode.Init(context);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver)
        {
            ActivateAnims(EnemyStates.Desperate);
            return;
        }
        
        mainNode.Run(context);
        RefreshAnimState();

    }

    public void SetState(EnemyStates newState)
    {
        if (enemyStates == newState) return;
        enemyStates = newState;
        GameManager.OnlyInstance.EnemyChangeMood(newState);
    }
    public void RefreshAnimState()
    {
        if (context.enemyAnimsStateInfo.isConfused)
        {
            ActivateAnims(EnemyStates.Confused);
            return;
        }
        if (context.enemyAnimsStateInfo.isAttacking)
        {
            ActivateAnims(EnemyStates.CatchingPlayer);
            return;
        }
        if (context.enemyAnimsStateInfo.isSurprised)
        {
            ActivateAnims(EnemyStates.Surprised);
        }
        if (aiAgent.velocity.magnitude > 0.2f)
        {
            if(aiAgent.speed > 4f)
            {
                ActivateAnims(EnemyStates.Running);
                return;
            }
            ActivateAnims(EnemyStates.Walking);
            return;
        }
        else if(!chasingPlayer && !gotDistraction)
        {
            ActivateAnims(EnemyStates.Idle);
            return;
        }
    }
    void ActivateAnims(EnemyStates newState)
    {
        if (currentAnimsState == newState) return;
        onEnemyStateChange?.Invoke(newState);
        currentAnimsState = newState;
    }
}
public class EnemyAnimsStateInfo
{
    public bool isConfused;
    public bool isAttacking;
    public bool isSurprised;

}
public enum EnemyStates
{
    Idle,
    Walking,
    Distracted,
    Confused,
    Surprised,
    Running,
    CatchingPlayer,
    Desperate
}
