using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitForCalling", menuName = "StateMachine/Nodes/WaitForCalling")]
public class SMNodeWaitForCalling : SMNode
{
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        if(context.movingTarget == null)
        {
            //context.enemy.SetState(EnemyStates.Idle);
        }
        context.encounteredPlayer = false;
        state = SMNodeStates.Failed;
        return state;
    }

}
