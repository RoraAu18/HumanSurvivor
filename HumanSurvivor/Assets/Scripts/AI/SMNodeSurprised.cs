using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Surprised", menuName = "StateMachine/Nodes/Surprised")]

public class SMNodeSurprised : SMNode
{
    float timer;
    [SerializeField]
    float surprisedTimerDelay;
    public override void Init(SMContext context)
    {
        base.Init(context);
        timer = 0;
    }
    public override SMNodeStates Run(SMContext context)
    {
        context.enemyAnimsStateInfo.isSurprised = false;
        if (timer == 0)
        {
            context.enemy.SetState(EnemyStates.Surprised);
            context.enemyAnimsStateInfo.isSurprised = true;
        }
        timer += Time.deltaTime;
        state = SMNodeStates.Running;
        if (timer >= surprisedTimerDelay)
        {
            state = SMNodeStates.Succeed;
        }
        return state;
    }
}
