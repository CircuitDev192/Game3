using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimWalk : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetFloat("Speed_f", 0.49f);
    }

    public override BaseState<PlayerAnimFSM> UpdateState(PlayerAnimFSM context)
    {
        context.weaponContext.currentWeapon.SetWalkValues(context.playerAnimator);

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return context.idleState;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            return context.jumpState;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            return context.runState;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return context.crouchState;
        }
        else
        {
            return context.walkState;
        }
    }

    public override void ExitState(PlayerAnimFSM context)
    {
        
    }
}
