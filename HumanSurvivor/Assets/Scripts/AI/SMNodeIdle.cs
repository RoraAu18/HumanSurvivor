using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "StateMachine/Nodes/Idle")]
public class SMNodeIdle : SMNode
{
    //Assign the method here (not before as it was an abstract one) it always has to return a state
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Succed;
        return state;
    }
 
}
