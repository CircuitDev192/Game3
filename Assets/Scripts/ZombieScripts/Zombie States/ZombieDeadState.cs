using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : ZombieBaseState
{
    public override void EnterState(ZombieContext context)
    {
        Debug.Log("Zombie entered Dead state!");
        EventManager.TriggerZombieKilled(context.gameObject);
        context.EnableRagdoll();
    }

    public override void ExitState(ZombieContext context)
    {
        
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        float distance = Vector3.Distance(context.transform.position, context.playerTransform.position);

        if (distance > context.deadDespawnDistance) return context.despawnState;

        return this;
    }
}
