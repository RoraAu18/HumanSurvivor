using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "StateMachine/Nodes/Attack")]
public class SMNodeAttack: SMNode
{
    float timer;
    float timeOverSpan;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }

    public override SMNodeStates Run(SMContext context)
    {
        context.enemyAnimsStateInfo.isAttacking = false;
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
        if (deltaPosition.magnitude <= context.lungeTargetDetection)
        {
            context.enemy.SetState(EnemyStates.CatchingPlayer);
            context.enemyAnimsStateInfo.isAttacking = true;
            GameManager.OnlyInstance.gameStates = GameStates.GameOver;
            Debug.Log("game over bitches");
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }

}
