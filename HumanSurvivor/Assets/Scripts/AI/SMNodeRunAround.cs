using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunAround", menuName = "StateMachine/Nodes/RunAround")]
public class SMNodeRunAround : SMNode
{
    [SerializeField]
    float runFor;
    float timer;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        context.movingTarget = null;
        if (!context.encounteredPlayer)
        {
            state = SMNodeStates.Failed;
            return state;
        }
        if (timer == 0) context.enemy.SetState(EnemyStates.Running);
        timer += Time.deltaTime;
        context.agentToMove.SetDestination(context.waypointSystem.currentWaypoint.position);
        //context.agentToMove.velocity
        state = SMNodeStates.Running;
        if (timer >= runFor)
        {
            state = SMNodeStates.Succeed;
        }
        return state;
    }

}
