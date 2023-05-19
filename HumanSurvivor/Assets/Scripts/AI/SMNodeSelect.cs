using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Select", menuName = "StateMachine/Nodes/Select")]
public class SMNodeSelect : SMNode
{
    public SMNode[] nodes;
    public int currNodeIdx;
    public override void Init(SMContext context)
    {
        base.Init(context);
        currNodeIdx = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Init(context);
        }
    }

    public override SMNodeStates Run(SMContext context)
    {

        RunCurrentNode(ref currNodeIdx, context);
        state = SMNodeStates.Failed;
        return state;

    }

    SMNodeStates RunCurrentNode(ref int nodeToRun, SMContext context)
    {
        var nodeState = nodes[nodeToRun].Run(context);
        if(nodeState == SMNodeStates.Succeed)
        {
            nodeToRun = 0;
            return state = SMNodeStates.Succeed;
        }
        else if(nodeState == SMNodeStates.Running)
        {
            state = SMNodeStates.Running;
            return state;
        }
        else if(nodeState == SMNodeStates.Failed)
        {
            nodeToRun++;
            if(nodeToRun >= nodes.Length)
            {
                nodeToRun = 0;
                return state = SMNodeStates.Failed;
            }
            else
            {
                return RunCurrentNode(ref nodeToRun, context);
            }
        }
        else
        {
            return state = SMNodeStates.Off;

        }
    }

}
