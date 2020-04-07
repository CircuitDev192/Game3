using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInvestigateState : ZombieBaseState
{
    public override void EnterState(ZombieContext context)
    {
        context.zombieNavMeshAgent.enabled = true;
        context.zombieNavMeshAgent.destination = context.soundLocation;
    }

    public override void ExitState(ZombieContext context)
    {
        context.heardSound = false;
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        float distance = Vector3.Distance(context.transform.position, context.playerTransform.position);

        if (distance > context.livingDespawnDistance) return context.despawnState;

        if (base.ShouldDie(context)) return context.deadState;

        if (base.SeesPlayer(context)) return context.chaseState;
        
        base.PlayTimedSound(context, this);

        if (context.zombieNavMeshAgent.remainingDistance <= context.zombieNavMeshAgent.stoppingDistance) return context.idleState;

        if (context.zombieNavMeshAgent.destination != context.soundLocation) context.zombieNavMeshAgent.destination = context.soundLocation;

        return this;
    }
}
