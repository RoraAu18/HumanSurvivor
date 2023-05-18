using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkAround", menuName = "StateMachine/Nodes/WalkAround")]
public class SMNodeWalkAround : SMNode
{
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
        if (!context.enemy.TryGetComponent(out WaypointStm waypointStm))
        {
            state = SMNodeStates.Failed;
            return state;
        }
        if(timer == 0) context.enemy.SetState(EnemyStates.Walking);
        timer += Time.deltaTime;
        context.agentToMove.SetDestination(context.waypointSystem.currentWaypoint.position);
        //context.agentToMove.velocity
        state = SMNodeStates.Running;
        if(timer >= walkFor)
        {
            state = SMNodeStates.Succeed;
        }
        return state;
    }
}
