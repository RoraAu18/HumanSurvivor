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
        if (!context.enemy.TryGetComponent(out WaypointUser waypointUser))
        {
            state = SMNodeStates.Failed;
            return state;
        }
        context.waypointTest.AddNewUser(context.enemy.GetComponent<WaypointUser>());
        state = SMNodeStates.Succeed;
        return state;
    }

}
