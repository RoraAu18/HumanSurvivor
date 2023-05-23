using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CloseToPlayer", menuName = "StateMachine/Nodes/CloseToPlayer")]

public class SMNodeCloseToPlayer : SMNode
{
    public AIPlayerController player;
    [SerializeField]
    SMNodeWorriedWalk worriedWalk;
    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
        deltaPosition.y = 0;
        if (context.movingTarget.TryGetComponent(out player))
        {
            Debug.Log("close to player");
            context.encounteredPlayer = true;
            state = SMNodeStates.Succeed;
            return state;
        }
        return state;
    }
}
