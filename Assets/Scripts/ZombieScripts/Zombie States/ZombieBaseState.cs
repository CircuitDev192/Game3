using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieBaseState : BaseState<ZombieContext>
{
    public bool ShouldDie(ZombieContext context)
    {        
        return (context.health <= 0);
    }

    public bool SeesPlayer(ZombieContext context)
    {
        Vector3 playerPosition = context.playerTransform.position;
        Vector3 zombiePosition = context.transform.position;

        float distance = Vector3.Distance(zombiePosition, playerPosition);

        // Is the player farther than we can see?
        if (distance > context.visionDistance) return false;

        Vector3 directionToPlayer = (playerPosition - zombiePosition).normalized;

        // Calculate the angle between the direction the zombie is facing and the player
        float angle = Mathf.Abs(Vector3.Angle(context.transform.forward, directionToPlayer));

        // Is the player in our FOV?
        if (angle < context.fieldOfView / 2f) return true;

        return false;
    }
}
