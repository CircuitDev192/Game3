using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimIdle : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetFloat("Speed_f", 0f);
    }

    public override BaseState<PlayerAnimFSM> UpdateState(PlayerAnimFSM context)
    {
        context.weaponContext.currentWeapon.SetIdleValues(context.playerAnimator);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return context.walkState;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            return context.jumpState;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return context.crouchState;
        }
        else
        {
            return context.idleState;
        }
    }

    public override void ExitState(PlayerAnimFSM context)
    {

    }
}
