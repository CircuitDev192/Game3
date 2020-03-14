using UnityEngine;

public class WeaponReloadState : WeaponBaseState
{
    private float timeToFinishReload;

    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered reload state");

        WeaponBase weapon = context.weapons[context.currentWeaponIndex];

        if (weapon.totalAmmo > 0)
        {
            context.playerAnimator.SetBool("Reload_b", true);
            weapon.Reload();
            timeToFinishReload = Time.time + weapon.reloadTime;
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
