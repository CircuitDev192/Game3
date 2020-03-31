using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDespawnState : ZombieBaseState
{
    public override void EnterState(ZombieContext context)
    {
        Debug.Log("Zombie entered Despawn state!");
        EventManager.TriggerZombieShouldDespawn(context.gameObject);
    }

    public override void ExitState(ZombieContext context)
    {
        
    }

    public override BaseState<ZombieContext> UpdateState(ZombieContext context)
    {
        return this;
    }
}
