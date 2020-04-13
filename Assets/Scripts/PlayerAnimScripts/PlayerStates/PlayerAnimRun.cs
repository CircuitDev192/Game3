using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimRun : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM player)
    {
        player.playerAnimator.SetFloat("Speed_f", 1f);
    }

    public override void Update(PlayerAnimFSM player)
    {
        player.weaponContext.currentWeapon.SetIdleValues(player.playerAnimator);

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            player.TransitionToState(player.idleState);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.TransitionToState(player.jumpState);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            player.TransitionToState(player.walkState);
        }
    }
}
