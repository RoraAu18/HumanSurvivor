using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorriedWalk", menuName = "StateMachine/Nodes/WorriedWalk")]

public class SMNodeWorriedWalk : SMNode
{
    public override SMNodeStates Run(SMContext context)
    {
        //If there´s no target, then tell that the action failed
        Debug.Log(context.movingTarget);
        if (context.movingTarget == null)
        {
            state = SMNodeStates.Failed;
            return state;
        }
        var targetPostNormalized = context.movingTarget.position;
        targetPostNormalized.y = context.agentToMove.transform.position.y;
        context.agentToMove.transform.LookAt(targetPostNormalized);
        context.agentToMove.SetDestination(context.movingTarget.transform.position);
        context.enemy.SetState(EnemyStates.Running);
        context.agentToMove.speed = 5;
        state = SMNodeStates.Succeed;
        return state;
    }
}
