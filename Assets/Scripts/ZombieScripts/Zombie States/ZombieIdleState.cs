using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieBaseState
{
    private float idleTime;
    private float timeToPatrol;

    public override void EnterState(ZombieContext context)
    {
        Debug.Log("Zombie entered Idle state!");
        context.zombieNavMeshAgent.enabled = false;
        context.zombieAnimator.SetFloat("Speed_f", 0f);

        idleTime = Random.Range(context.minimumIdleTime, context.maximumIdleTime);
        timeToPatrol = Time.time + idleTime;
    }

    public override void ExitState(ZombieContext context)
    {
        
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        if (context.heardSound) return context.investigateState;

        float distance = Vector3.Distance(context.transform.position, context.playerTransform.position);

        if (distance > context.livingDespawnDistance) return context.despawnState;

        if (base.ShouldDie(context)) return context.deadState;

        if (base.SeesPlayer(context)) return context.chaseState;

        if (Time.time > timeToPatrol) return context.patrolState;
        
        base.PlayTimedSound(context, this);

        return this;
    }
}
