using UnityEngine;

public class WeaponSwapState : WeaponBaseState
{
    private float timeToFinishSwap;

    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered swap state.");
                
        context.currentWeapon.enabled = false;

        if (!context.consumableEquipped)
        {
            if (context.currentScrollDelta != 0)
            {
                context.currentWeaponIndex = (context.currentWeaponIndex - 1 * (int)Mathf.Sign(context.currentScrollDelta) + context.weapons.Count) % context.weapons.Count;
            }

            context.currentWeapon = context.weapons[context.currentWeaponIndex];
            context.currentWeapon.flashlightOn = context.flashlightOn;
            context.currentWeapon.enabled = true;
        }
        else
        {
            context.currentWeapon = context.consumables[context.currentConsumableIndex];
        }


        EventManager.TriggerWeaponChanged(context.currentWeapon.name);
        EventManager.TriggerAmmoCountChanged(context.currentWeapon.roundsInCurrentMag);
        EventManager.TriggerTotalAmmoChanged(context.currentWeapon.totalAmmo);

        context.playerAnimator.SetInteger("WeaponType_int", context.currentWeapon.weaponAnimation);
        context.playerAnimator.SetInteger("MeleeType_int", context.currentWeapon.meleeType);
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
