using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFleeState : ZombieBaseState
{
    private float fleeDistance;
    private float maxSegmentDistance = 5f;
    private int segments;
    private int currentSegment = 0;
    private float distancePerSegment;

    public override void EnterState(ZombieContext context)
    {
        Debug.Log("Zombie entered flee state!");

        currentSegment = 0;
        context.zombieNavMeshAgent.enabled = true;
        context.zombieNavMeshAgent.speed = context.runSpeed;


        fleeDistance = context.fleeVector.magnitude;
        segments = Mathf.RoundToInt(fleeDistance / maxSegmentDistance);
        distancePerSegment = fleeDistance / (float)segments;

        Vector3 fleeDestination = context.transform.position + distancePerSegment * context.fleeVector.normalized;

        context.zombieNavMeshAgent.destination = fleeDestination;

        context.zombieAnimator.SetFloat("Speed_f", context.runSpeed);
    }

    public override void ExitState(ZombieContext context)
    {
        
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        //base.ShouldFlee(context);
        if (context.zombieNavMeshAgent.remainingDistance < context.zombieNavMeshAgent.stoppingDistance)
        {
            currentSegment++;

            if(currentSegment == segments) return context.idleState;

            Vector3 fleeDestination = context.transform.position + distancePerSegment * context.fleeVector.normalized;

            context.zombieNavMeshAgent.destination = fleeDestination;
        }

        return this;
    }
}
