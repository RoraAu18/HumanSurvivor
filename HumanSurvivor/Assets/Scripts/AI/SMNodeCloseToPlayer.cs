using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CloseToPlayer", menuName = "StateMachine/Nodes/CloseToPlayer")]

public class SMNodeCloseToPlayer : SMNode
{
    public AIPlayerController player;

    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
        if (deltaPosition.magnitude <= 2 && context.movingTarget.TryGetComponent(out player))
        {
            Debug.Log("close to player");
            context.encounteredPlayer = true;
            context.enteringWayPoint = false;
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }
}
