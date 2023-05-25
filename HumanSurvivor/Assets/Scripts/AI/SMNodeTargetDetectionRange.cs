using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetDetection", menuName = "StateMachine/Nodes/TargetDetection")]
public class SMNodeTargetDetectionRange : SMNode
{
    AIPlayerController player;
    [SerializeField]
    float limitAngle = 45;
    [SerializeField]
    LayerMask maskLayer;
    [SerializeField]
    RaycastHit[] hits = new RaycastHit[5];
    [SerializeField]
    float innerDetectionRadius = 7;
    [SerializeField]
    float sneakerDetectionRadius = 0.5f;



    public override void Init(SMContext context)
    {
        base.Init(context);
    }
    public override SMNodeStates Run(SMContext context)
    {
        context.enemy.chasingPlayer = false;

        context.movingTarget = null;
        state = SMNodeStates.Failed;


        var currentCollisions = context.collisionControler.currentFrameCollissions;
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            var currentCollis = currentCollisions[i];
            if (currentCollis.TryGetComponent(out player))
            {
                break;
            }
        }
        if (!player) return state;

        var dir = player.transform.position - context.agentToMove.transform.position;

        var selectedDetectionRadius = innerDetectionRadius;
        if (player.onStealhMode)
        {
            selectedDetectionRadius = sneakerDetectionRadius;
        }

        var angle = Vector3.Angle(dir, context.agentToMove.transform.forward);
        if (angle > limitAngle && dir.magnitude > selectedDetectionRadius) return state;

        //avoid walls blinding when encountering walls
        var hitCount = Physics.RaycastNonAlloc(context.agentToMove.transform.position, dir, hits, dir.magnitude, maskLayer);
        if (hitCount > 0) return state;

        context.movingTarget = player.transform;
        context.enemy.chasingPlayer = true;
        Debug.Log("dectect at " + selectedDetectionRadius);
        state = SMNodeStates.Succeed;
        return state;
    }
}
