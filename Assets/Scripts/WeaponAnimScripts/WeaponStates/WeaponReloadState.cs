using UnityEngine;

public class WeaponReloadState : WeaponBaseState
{
    private float timeToFinishReload;

    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered reload state");

        if (context.currentWeapon.totalAmmo > 0)
        {
            context.playerAnimator.SetBool("Reload_b", true);
            context.currentWeapon.Reload();
            timeToFinishReload = Time.time + context.currentWeapon.reloadTime;
        }
        else timeToFinishReload = Time.time;
    }

    public override void ExitState(WeaponContext context)
    {
        Debug.Log("Weapon exited reload state");

        context.playerAnimator.SetBool("Reload_b", false);
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        if (Time.time > timeToFinishReload) return context.idleState;

        return this;
    }
}
