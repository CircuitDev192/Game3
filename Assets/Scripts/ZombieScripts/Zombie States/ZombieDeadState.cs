using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : ZombieBaseState
{
    public override void EnterState(ZombieContext context)
    {
        Debug.Log("Zombie entered Dead state!");
        EventManager.TriggerZombieKilled(context.gameObject);
        context.despawnTimer = 10f;
        context.EnableRagdoll();
    }

    public override void ExitState(ZombieContext context)
    {
        
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        float distance = Vector3.Distance(context.transform.position, context.playerTransform.position);

        context.despawnTimer -= Time.deltaTime;

        if (distance > context.deadDespawnDistance || context.despawnTimer <= 0f) return context.despawnState;

        return this;
    }
}
