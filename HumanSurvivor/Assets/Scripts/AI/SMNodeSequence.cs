using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "StateMachine/Nodes/Sequence")]
public class SMNodeSequence : SMNode
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
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver) return state = SMNodeStates.Succeed;
        for (int i = 0; i < nodes.Length; i++)
        {
            //From the Run of the SMNode check if it failed 
            if (nodes[i].Run(context) == SMNodeStates.Failed)
            {
                //Return the status of the sequence as failed
                //Debug.Log(nodes[i] + " " + nodes[i].state);
                state = SMNodeStates.Failed;
                return state;
            }
        }
        //If none of the Nodes fail, then it´ll managed to succeed
        state = SMNodeStates.Succeed;
        return state;
    }

}
