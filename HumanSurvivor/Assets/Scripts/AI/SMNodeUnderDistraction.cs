using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnderDistraction", menuName = "StateMachine/Nodes/UnderDistraction")]
public class SMNodeUnderDistraction : SMNode
{
    [SerializeField]
    Transform distractionTarget;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        //partial condition
        if (context.enemy.gotDistraction)
        {
            distractionTarget = context.distractionTarget;
            context.movingTarget = distractionTarget.transform;
            state = SMNodeStates.Succeed;
        }
        return state;
    }
}
