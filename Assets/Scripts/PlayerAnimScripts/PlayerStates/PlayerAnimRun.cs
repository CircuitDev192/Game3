using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimRun : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetFloat("Speed_f", 1f);
    }

    public override BaseState<PlayerAnimFSM> UpdateState(PlayerAnimFSM context)
    {
        context.weaponContext.currentWeapon.SetRunValues(context.playerAnimator);

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            return context.idleState;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            return context.jumpState;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || !Input.GetKey(KeyCode.W))
        {
            return context.walkState;
        }
        else
        {
            return context.runState;
        }
    }

    public override void ExitState(PlayerAnimFSM context)
    {
        
    }
}
