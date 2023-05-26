using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkAround", menuName = "StateMachine/Nodes/WalkAround")]
public class SMNodeWalkAround : SMNode
{
    [SerializeField]
    SMNodeTargetDetectionRange targetDetectionRange;    
    [SerializeField]
    SMNodeUnderDistraction underDistraction;
    [SerializeField]
    float walkFor;
    float timer;
    public override void Init(SMContext context)
    {
        timer = 0;
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        context.movingTarget = null;
        var detectionNode = targetDetectionRange.Run(context);
        if (detectionNode == SMNodeStates.Succeed) return state = SMNodeStates.Failed;
        var underdist = underDistraction.Run(context);
        if(underdist == SMNodeStates.Succeed) return state = SMNodeStates.Failed;
        if (!context.enemy.TryGetComponent(out WaypointStm waypointStm))
        {
            state = SMNodeStates.Failed;
            return state;
        }
        if(timer == 0) context.enemy.SetState(EnemyStates.Walking);
        timer += Time.deltaTime;
        context.agentToMove.speed = 3f;
        context.agentToMove.SetDestination(context.waypointSystem.currentWaypoint.position);
        state = SMNodeStates.Running;
        if(timer >= walkFor)
        {
            context.enemyAnimsStateInfo.isConfused = false;
            state = SMNodeStates.Succeed;
        }
        return state;
    }
}
