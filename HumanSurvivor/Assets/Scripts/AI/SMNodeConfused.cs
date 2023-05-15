using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Confused", menuName = "StateMachine/Nodes/Confused")]

public class SMNodeConfused : SMNode
{
    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        context.enemy.SetState(EnemyStates.Confused);
        Debug.Log("Confused and should go back to idle");
        state = SMNodeStates.Failed;
        context.movingTarget = null;

        return state;
    }

}
