using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CloseToDistraction", menuName = "StateMachine/Nodes/CloseToDistraction")]

public class SMNodeCloseToDistraction : SMNode
{
    [SerializeField]
    float confusedTime;
    float timer;
    public override void Init(SMContext context)
    {
        base.Init(context);
        timer = 0;

    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        timer += Time.deltaTime;
        var deltaPosition = context.agentToMove.transform.position - GameManager.OnlyInstance.currentDistraction.position;
        if(deltaPosition.magnitude <= context.lungeTargetDetection || timer >= confusedTime)
        {
            context.gotToDistraction = true;
            context.enemy.gotDistraction = false;
            timer = 0;
            state = SMNodeStates.Succeed;
            return state;
            
        }
        return state;
    }
}
