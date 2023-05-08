using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "StateMachine/Nodes/Attack")]
public class SMNodeAttack: SMNode
{
    public float lifeSpan = 10;
    public float damageCounter = 10;
    public float damage = 1;
    public float attackDetectionRadius= 3;
    DamageController damageController;
    
    public override SMNodeStates Run(SMContext context)
    {
        state = SMNodeStates.Failed;

        var deltaPosition = context.agentToMove.transform.position - context.movingTarget.transform.position;
    
        if (deltaPosition.magnitude <= attackDetectionRadius && context.movingTarget.TryGetComponent(out damageController))
        {
            damageController.GetDamage(damage);
            //damageCounter -= damage;
            state = SMNodeStates.Succed;
            return state;
        }
        return state;
    }

}
