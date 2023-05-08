using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Walk", menuName = "StateMachine/Nodes/Walk")]
public class SMNodeWalk : SMNode
{
    public override SMNodeStates Run(SMContext context)
    {
        //If there´s no target, then tell that the action failed
        if (context.movingTarget == null)
        {
            state = SMNodeStates.Failed;
            context.agentToMove.isStopped = true;
            return state;
        }

        var targetPostNormalized = context.movingTarget.position;
        targetPostNormalized.y = context.agentToMove.transform.position.y;
        context.agentToMove.transform.LookAt(targetPostNormalized);

        context.agentToMove.SetDestination(context.movingTarget.position);
        state = SMNodeStates.Succed;
        return state;
    }
}
