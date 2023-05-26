using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Walk", menuName = "StateMachine/Nodes/Walk")]
public class SMNodeWalk : SMNode
{
    [SerializeField]
    SMNodeTargetDetectionRange targetDetectionRange;
    public override SMNodeStates Run(SMContext context)
    {
        //If there´s no target, then tell that the action failed
        if (context.movingTarget == null)
        {
            state = SMNodeStates.Failed;
            context.agentToMove.isStopped = true;
            return state;
        }

        //var detectionNode = targetDetectionRange.Run(context);
        //if (detectionNode == SMNodeStates.Succeed) return state = SMNodeStates.Failed;

        var targetPostNormalized = context.movingTarget.position;
        targetPostNormalized.y = context.agentToMove.transform.position.y;
        context.agentToMove.transform.LookAt(targetPostNormalized);
        Debug.Log("walking");
        context.agentToMove.speed = 3f;

        context.agentToMove.SetDestination(context.movingTarget.transform.position);
        if (!context.gotToDistraction)
        {
            context.enemy.SetState(EnemyStates.Walking);
        }

        var dis = context.agentToMove.transform.position - context.movingTarget.transform.position;
        if(dis.magnitude > context.lungeTargetDetection)
        {
            state = SMNodeStates.Succeed;
        }
        return state;

    }
}
