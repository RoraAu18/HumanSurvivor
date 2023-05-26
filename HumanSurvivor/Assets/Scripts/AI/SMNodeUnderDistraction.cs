using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnderDistraction", menuName = "StateMachine/Nodes/UnderDistraction")]
public class SMNodeUnderDistraction : SMNode
{
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        if(context.movingTarget == null)
        {
            state = SMNodeStates.Failed;
        }
        state = SMNodeStates.Failed;
        if (context.enemy.gotDistraction)
        {
            context.movingTarget = GameManager.OnlyInstance.currentDistraction;
            Debug.Log("distraction target is " + GameManager.OnlyInstance.currentDistraction);
            state = SMNodeStates.Succeed;
        }
        return state;
    }
}
