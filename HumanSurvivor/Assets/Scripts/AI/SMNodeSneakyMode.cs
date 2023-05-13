using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SneakyMode", menuName = "StateMachine/Nodes/SneakyMode")]
public class SMNodeSneakyMode : SMNode
{

    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;
        var sneakyButton = Input.GetKeyDown(KeyCode.Q);

        if (sneakyButton)
        {
            state = SMNodeStates.Succeed;
            Debug.Log("I'm in Sneaky mode");
        }
        //23 may pasar a servi med normanndia lun a vie 8 a 4
    
        return state;
    }

}
