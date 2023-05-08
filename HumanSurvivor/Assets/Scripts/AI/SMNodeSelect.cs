using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Select", menuName = "StateMachine/Nodes/Select")]
public class SMNodeSelect : SMNode
{
    public SMNode[] nodes;

    public override void Init(SMContext context)
    {
        base.Init(context);
        for (int i = 0; i < nodes.Length; i++)
        {
            Init(context);
        }
    }

    public override SMNodeStates Run(SMContext context)
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].Run(context) == SMNodeStates.Succed)
            {
                state= SMNodeStates.Succed;
                return state;
            }
        }
        state = SMNodeStates.Failed;
        return state;


    }
}
