using UnityEngine;

public class PlayerAnimDead : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM player)
    {
        //Debug.Log("Player entered dead state.");
        player.GetComponent<FirstPersonMovement>().enabled = false;
        player.playerAnimator.SetBool("Death_b", true);
        EventManager.TriggerPlayerKilled();
    }

    public override void Update(PlayerAnimFSM player)
    {
        return;
    }
}
