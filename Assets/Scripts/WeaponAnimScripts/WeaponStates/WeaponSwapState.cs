using UnityEngine;

public class WeaponSwapState : WeaponBaseState
{
    private float timeToFinishSwap;

    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered swap state.");

        WeaponBase weapon = context.weapons[context.currentWeaponIndex];
        weapon.enabled = false;

        context.currentWeaponIndex = (context.currentWeaponIndex + 1) % context.weapons.Count;

        weapon = context.weapons[context.currentWeaponIndex];
        weapon.flashlightOn = context.flashlightOn;
        weapon.enabled = true;

        EventManager.TriggerWeaponChanged(weapon.name);
        EventManager.TriggerAmmoCountChanged(weapon.roundsInCurrentMag);

        context.playerAnimator.SetInteger("WeaponType_int", weapon.weaponAnimation);
        context.playerAnimator.SetInteger("MeleeType_int", weapon.meleeType);
        timeToFinishSwap = Time.time + context.weaponSwapTime;
    }

    public override void ExitState(WeaponContext context)
    {
        Debug.Log("Weapon exited swap state.");
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        if (Time.time > timeToFinishSwap) return context.idleState;

        return this;
    }
}
