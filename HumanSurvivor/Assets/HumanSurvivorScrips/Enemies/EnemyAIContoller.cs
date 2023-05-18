using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAIContoller : MonoBehaviour
{
    public SMNode mainNode;
    public bool gotDistraction;

    [SerializeField]
    SMContext context;
    [SerializeField]
    EnemyStates enemyStates;
    EnemyStates oldEnemyStates;
    Rigidbody rb;
    public Action<EnemyStates> onEnemyStateChange;
    void Start()
    {
        TryGetComponent(out rb);
        mainNode.Init(context);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SetState(EnemyStates.Distracted);
        }
        mainNode.Run(context);
        //context.distractionTarget = GameManager.OnlyInstance.currentDistraction;
    }

    public void SetState(EnemyStates newState)
    {
        if (enemyStates == newState) { Debug.Log("going through " + enemyStates); return; }
        enemyStates = newState;
        ActivateAnims(enemyStates);
        GameManager.OnlyInstance.EnemyChangeMood(newState);
    }

    void ActivateAnims(EnemyStates newState)
    {
        if (oldEnemyStates == newState) return;
        enemyStates = newState;
        onEnemyStateChange?.Invoke(enemyStates);
        oldEnemyStates = enemyStates;
    }
}

public enum EnemyStates
{
    Idle,
    Walking,
    Distracted,
    Confused,
    Surprised,
    Running,
    CatchingPlayer
}
