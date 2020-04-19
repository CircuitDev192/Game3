using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCrouch : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetBool("Crouch_b", true);
    }

    public override BaseState<PlayerAnimFSM> UpdateState(PlayerAnimFSM context)
    {
        context.weaponContext.currentWeapon.SetWalkValues(context.playerAnimator);

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            return context.idleState;
        }
        else
        {
            return context.crouchState;
        }
    }

    public override void ExitState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetBool("Crouch_b", false);
    }
}
