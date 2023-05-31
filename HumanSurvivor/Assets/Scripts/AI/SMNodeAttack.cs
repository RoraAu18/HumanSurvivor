using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "StateMachine/Nodes/Attack")]
public class SMNodeAttack: SMNode, IWinLoseStateUser
{
    bool onLost;
    public override void Init(SMContext context)
    {
        base.Init(context);
        GameManager.OnlyInstance.winLoseStateUser.Add(this);
    }

    public override SMNodeStates Run(SMContext context)
    {
        onLost = false;

        context.enemyAnimsStateInfo.isAttacking = false;
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
        if (deltaPosition.magnitude <= context.lungeTargetDetection)
        {
            context.enemy.SetState(EnemyStates.CatchingPlayer);
            context.enemyAnimsStateInfo.isAttacking = true;
            this.WinLoseEvent(false);
            if (onLost)
            {
                Debug.Log("entering onlost");
                context.enemy.SetState(EnemyStates.Idle);
            }
            //GameManager.OnlyInstance.gameStates = GameStates.GameOver;
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }

    public void WinLoseEvent(bool youWin)
    {
        onLost = true;
    }
}
