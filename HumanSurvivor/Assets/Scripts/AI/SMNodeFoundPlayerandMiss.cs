using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckIfPlayerWasFound", menuName = "StateMachine/Nodes/CheckIfPlayerWasFound")]
public class SMNodeFoundPlayerandMiss : SMNode
{
    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        if (context.encounteredPlayer && context.movingTarget == null)
        {
            Debug.Log("missed player");
            state = SMNodeStates.Succeed;
        }
        return state;
    }
}
