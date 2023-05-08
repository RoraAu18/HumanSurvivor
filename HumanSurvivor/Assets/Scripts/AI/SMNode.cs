using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMNode : ScriptableObject
{
    //Create a reference for the class to keep track of the obejt´s results
    public SMNodeStates state;

    public virtual void Init(SMContext context)
    {
        state = SMNodeStates.Off;
    }
    public virtual SMNodeStates Run(SMContext context)
    {
        Debug.Log("State not defined");
        state = SMNodeStates.Failed;
        return state;
    }
}

[Serializable]
public class SMContext
{
    public NavMeshAgent agentToMove;
    public Transform movingTarget;
    public ColisionController collisionControler;
  
}

public enum SMNodeStates
{
    //represents the name (string) of a state and the number that corresponds to that name
    Off = 0,
    Running = 1,
    Succed = 2,
    Failed = 3
}
