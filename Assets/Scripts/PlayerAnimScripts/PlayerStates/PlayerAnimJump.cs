using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimJump : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetBool("Jump_b", true);
    }

    public override BaseState<PlayerAnimFSM> UpdateState(PlayerAnimFSM context)
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            return context.idleState;
        }
        else
        {
            return context.jumpState;
        }
    }

    public override void ExitState(PlayerAnimFSM context)
    {
        context.playerAnimator.SetBool("Jump_b", false);
    }
}
