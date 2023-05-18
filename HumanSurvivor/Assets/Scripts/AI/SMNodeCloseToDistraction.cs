using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CloseToDistraction", menuName = "StateMachine/Nodes/CloseToDistraction")]

public class SMNodeCloseToDistraction : SMNode
{
    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.distractionTarget.transform.position;
        if(deltaPosition.magnitude <= context.lungeTargetDetection)
        {
            context.gotToDistraction = true;
            context.enemy.gotDistraction = false;
            context.encounteredPlayer = false;
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }
}
