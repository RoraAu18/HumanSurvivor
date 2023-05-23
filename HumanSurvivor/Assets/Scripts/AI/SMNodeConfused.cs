using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Confused", menuName = "StateMachine/Nodes/Confused")]


public class SMNodeConfused : SMNode
{
    [SerializeField]
    SMNodeTargetDetectionRange targetDetectionRange;
    float timer;
    [SerializeField]
    float timeSpan;
    public override void Init(SMContext context)
    {
        base.Init(context);
        timer = 0;
    }
    public override SMNodeStates Run(SMContext context)
    {
        context.enemyAnimsStateInfo.isConfused = false;
        var detectionNode = targetDetectionRange.Run(context);
        if (detectionNode == SMNodeStates.Succeed) return state = SMNodeStates.Failed;
        if (timer == 0)
        {
            context.enemy.SetState(EnemyStates.Confused);
            context.enemyAnimsStateInfo.isConfused = true;
        }
        state = SMNodeStates.Running;
        timer += Time.deltaTime;
        if(timer >= timeSpan)
        {
            state = SMNodeStates.Succeed;
        }
        
        Debug.Log("Confused and should go back to idle");
        return state;
    }


}
