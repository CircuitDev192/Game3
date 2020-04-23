using UnityEngine;

public class WeaponReloadState : WeaponBaseState
{
    private float timeToFinishReload;

    public override void EnterState(WeaponContext context)
    {

        if (PlayerManager.instance.GetTotalAmmoOfType(context.currentWeapon.ammoType) > 0)
        {
            context.playerAnimator.SetBool("Reload_b", true);
            context.currentWeapon.Reload();
            timeToFinishReload = Time.time + context.currentWeapon.reloadTime;
        }
        else timeToFinishReload = Time.time;
    }

    public override void ExitState(WeaponContext context)
    {
        context.playerAnimator.SetBool("Reload_b", false);
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        base.ManageFlashlightDrain(context);

        if (Time.time > timeToFinishReload) return context.idleState;

        return this;
    }
}
