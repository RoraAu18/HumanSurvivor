using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoToEntrance", menuName = "StateMachine/Nodes/GoToEntrance")]
public class SMNodeGoToEntrance : SMNode
{
    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        /*
        if(context.waypointUser.currentHouseRoom != null)
        {
            var currentEntrance = context.waypointUser.currentHouseRoom.GetComponentInChildren<HouseRoom>();
            //way
            state = SMNodeStates.Succeed;
        }*/
        return state;
    }
}
