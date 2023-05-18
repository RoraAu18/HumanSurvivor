using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "StateMachine/Nodes/Sequence")]
public class SMNodeSequence : SMNode
{
    public SMNode[] nodes;
    int currentIdx;
    public override void Init(SMContext context)
    {
        base.Init(context);
        currentIdx = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Init(context);
        }
    }
    public override SMNodeStates Run(SMContext context)
    {
        return RunCurrentNode(ref currentIdx, context);

    }
    SMNodeStates RunCurrentNode(ref int nodeToRun, SMContext context)
    {
        SMNodeStates nodeState = nodes[nodeToRun].Run(context);
        switch (nodeState)
        {
            case SMNodeStates.Failed:
                return state = SMNodeStates.Failed;
            case SMNodeStates.Succeed:
                nodeToRun++;
                if (nodeToRun < nodes.Length)
                {
                    return RunCurrentNode(ref nodeToRun, context);
                }
                nodeToRun = 0;
                return SMNodeStates.Succeed;
            case SMNodeStates.Running:
                return SMNodeStates.Running;
            default:
                return SMNodeStates.Off;

        }
    }
}
