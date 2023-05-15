using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "StateMachine/Nodes/Attack")]
public class SMNodeAttack: SMNode
{
    public float attackDetectionRadius = 1;
    public AIEnemyController player;
    
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;

        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
    
        if (deltaPosition.magnitude <= attackDetectionRadius && context.movingTarget.TryGetComponent(out player))
        {
            context.enemy.SetState(EnemyStates.CatchingPlayer);
            Debug.Log("game over bitches");
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }

}
