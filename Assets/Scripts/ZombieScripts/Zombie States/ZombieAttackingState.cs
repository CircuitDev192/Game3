using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackingState : ZombieBaseState
{
    private float attackDelay = 1f;
    private float timeToAttack;

    public override void EnterState(ZombieContext context)
    {
        timeToAttack = Time.time;
    }

    public override void ExitState(ZombieContext context)
    {
        
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        if (base.ShouldDie(context)) return context.deadState;

        float distance = Vector3.Distance(context.transform.position, context.playerTransform.position);

        if (distance > context.zombieNavMeshAgent.stoppingDistance) return context.chaseState;

        if(Time.time > timeToAttack)
        {
            context.playerTransform.gameObject.GetComponent<IDamageAble>().Damage(context.damage);
            timeToAttack = Time.time + attackDelay;
        }

        if (context.playerDead) context.zombieAnimator.SetBool("Eating_b", true);

        return this;
    }
}
