using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetDetection", menuName = "StateMachine/Nodes/TargetDetection")]
public class SMNodeTargetDetectionRange : SMNode
{
    public AIPlayerController player;
    public float limitAngle = 45;
    public LayerMask maskLayer;
    public RaycastHit[] hits = new RaycastHit[5];
    public float innerDetectionRadius = 4;
    public float sneakerDetectionRadius = 2;
    public float OuterDetectionRadius = 10;

    public override void Init(SMContext context)
    {
        base.Init(context);
        context.collisionControler.theCollider.radius = OuterDetectionRadius;
    }
    public override SMNodeStates Run(SMContext context)
    {
        context.movingTarget = null;
        state = SMNodeStates.Failed;
        //var sneakyButton = 

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
        if (player.OnSneakyMode)
        {
            selectedDetectionRadius = sneakerDetectionRadius;
        }

        var angle = Vector3.Angle(dir, context.agentToMove.transform.forward);
        if (angle > limitAngle && dir.magnitude > selectedDetectionRadius) return state;

        //avoid walls blinding when encountering walls
        var hitCount = Physics.RaycastNonAlloc(context.agentToMove.transform.position, dir, hits, dir.magnitude, maskLayer);
        if (hitCount > 0) return state;


        context.movingTarget = player.transform;
        state = SMNodeStates.Succed;
        return state;


    }
    

}
