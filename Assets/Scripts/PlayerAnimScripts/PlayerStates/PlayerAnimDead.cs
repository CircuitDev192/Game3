using UnityEngine;

public class PlayerAnimDead : PlayerAnimBase
{
    public override void EnterState(PlayerAnimFSM context)
    {
        //Debug.Log("Player entered dead state.");
        context.GetComponent<FirstPersonMovement>().enabled = false;
        context.GetComponent<WeaponController>().enabled = false;
        context.playerAnimator.SetBool("Death_b", true);
        EventManager.TriggerPlayerKilled();
    }

    public override BaseState<PlayerAnimFSM> UpdateState(PlayerAnimFSM context)
    {
        return context.deadState;
    }

    public override void ExitState(PlayerAnimFSM context)
    {
        
    }
}
