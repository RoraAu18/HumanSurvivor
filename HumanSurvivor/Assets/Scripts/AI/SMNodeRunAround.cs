using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunAround", menuName = "StateMachine/Nodes/RunAround")]
public class SMNodeRunAround : SMNode
{
    [SerializeField]
    SMNodeTargetDetectionRange targetDetectionRange;
    [SerializeField]
    SMNodeUnderDistraction underDistraction;
    [SerializeField]
    float runFor;
    float timer;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        var detectionNode = targetDetectionRange.Run(context);
        if (detectionNode == SMNodeStates.Succeed) return state = SMNodeStates.Failed;
        var underdist = underDistraction.Run(context);
        if (underdist == SMNodeStates.Succeed) return state = SMNodeStates.Failed;
        if (!context.encounteredPlayer)
        {
            state = SMNodeStates.Failed;
            return state;
        }
        if (timer == 0) context.enemy.SetState(EnemyStates.Running);
        timer += Time.deltaTime;
        context.agentToMove.speed = 10f;
        context.agentToMove.SetDestination(context.waypointSystem.currentWaypoint.position);
        state = SMNodeStates.Running;
        if (timer >= runFor)
        {
            state = SMNodeStates.Succeed;
        }
        return state;
    }

}
