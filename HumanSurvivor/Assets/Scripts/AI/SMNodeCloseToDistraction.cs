using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CloseToDistraction", menuName = "StateMachine/Nodes/CloseToDistraction")]

public class SMNodeCloseToDistraction : SMNode
{
    Dictionary<int, GameObject> sksk = new Dictionary<int, GameObject>();
    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
        if(deltaPosition.magnitude <= context.lungeTargetDetection)
        //if (deltaPosition.magnitude <= context.lungeTargetDetection && context.movingTarget.transform == context.distractionTarget.transform)
        {
            Debug.Log("close to distraction");

            context.gotToDistraction = true;
            context.enemy.gotDistraction = false;
            context.encounteredPlayer = false;
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }
}
