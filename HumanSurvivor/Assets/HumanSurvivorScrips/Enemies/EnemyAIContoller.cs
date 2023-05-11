using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAIContoller : MonoBehaviour, IWaypointUser
{
    [SerializeField]
    public SMNode mainNode;
    [SerializeField]
    SMContext context;
    [SerializeField]
    EnemyStates enemyStates;
    Rigidbody rb;
    public Action<EnemyStates> onEnemyStateChange;
    void Start()
    {
        TryGetComponent(out rb);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SetState(EnemyStates.Distracted);
        }
        mainNode.Run(context);
        
    }

    void SetState(EnemyStates newState)
    {
        if (enemyStates == newState) return;
        enemyStates = newState;
        onEnemyStateChange?.Invoke(enemyStates);
    }

    public bool ShouldChangeWaypoint()
    {
        throw new NotImplementedException();
    }

    public float TimeToMove()
    {
        throw new NotImplementedException();
    }

    public bool ShouldStartOver()
    {
        throw new NotImplementedException();
    }
}

public enum EnemyStates
{
    Idle,
    Distracted,
    Surprised,
    Running,
    CatchingPlayer
}
