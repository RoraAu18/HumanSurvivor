using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "StateMachine/Nodes/Attack")]
public class SMNodeAttack: SMNode
{
    AIEnemyController player;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
        if (deltaPosition.magnitude <= context.lungeTargetDetection && context.movingTarget.TryGetComponent(out player))
        {
            context.enemy.SetState(EnemyStates.CatchingPlayer);
            Debug.Log("game over bitches");
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }

}
