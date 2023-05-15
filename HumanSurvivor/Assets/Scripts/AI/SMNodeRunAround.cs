using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunAround", menuName = "StateMachine/Nodes/RunAround")]
public class SMNodeRunAround : SMNode
{
    [SerializeField]
    float runSpeed;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        Debug.Log("calling RunAround");
        context.movingTarget = null;
        if (!context.enemy.TryGetComponent(out WaypointUser waypointUser))
        {
            state = SMNodeStates.Failed;
            return state;
        }
        if (!context.enteringWayPoint)
        {
            context.waypointUser.Init();
            context.waypointUser.systemActive = true;
            context.enteringWayPoint = true;
            context.enemy.SetState(EnemyStates.Running);

        }

        state = SMNodeStates.Succeed;
        return state;
    }

}
