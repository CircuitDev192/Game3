using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimJump : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM player)
    {
        player.playerAnimator.SetBool("Jump_b", true);
    }

    public override void Update(PlayerAnimFSM player)
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            player.playerAnimator.SetBool("Jump_b", false);
            player.TransitionToState(player.idleState);
        }
    }
}
