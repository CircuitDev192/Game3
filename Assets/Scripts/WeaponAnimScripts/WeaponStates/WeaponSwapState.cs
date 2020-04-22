using UnityEngine;

public class WeaponSwapState : WeaponBaseState
{
    private float timeToFinishSwap;

    public override void EnterState(WeaponContext context)
    {
        context.currentWeapon.audioSource.PlayOneShot(context.currentWeapon.holsterSound, 0.5f * PlayerManager.instance.soundMultiplier);
        
        context.currentWeapon.enabled = false;

        if (!context.consumableEquipped)
        {
            if (context.currentScrollDelta != 0)
            {
                context.currentWeaponIndex = (context.currentWeaponIndex - 1 * (int)Mathf.Sign(context.currentScrollDelta) + context.weapons.Count) % context.weapons.Count;
            }

            context.currentWeapon = context.weapons[context.currentWeaponIndex];
            context.currentWeapon.enabled = true;
            context.currentWeapon.flashlightOn = context.flashlightOn;
            context.currentWeapon.flashLight.enabled = context.flashlightOn;
        }
        else
        {
            context.currentWeapon = context.consumables[context.currentConsumableIndex];
            context.currentWeapon.enabled = true;
        }


        EventManager.TriggerWeaponChanged(context.currentWeapon.name);
        EventManager.TriggerAmmoCountChanged(context.currentWeapon.roundsInCurrentMag);
        EventManager.TriggerTotalAmmoChanged(PlayerManager.instance.GetTotalAmmoOfType(context.currentWeapon.ammoType), context.currentWeapon.ammoType);

        if (context.currentWeapon.equippedSuppressor)
        {
            context.currentWeapon.equippedSuppressor.GetComponent<Suppressor>().UpdateDurability(0);
        } else
        {
            EventManager.TriggerSuppressorDurabilityChanged(0f);
        }

        context.playerAnimator.SetInteger("WeaponType_int", context.currentWeapon.weaponAnimation);
        context.playerAnimator.SetInteger("MeleeType_int", context.currentWeapon.meleeType);
        timeToFinishSwap = Time.time + context.weaponSwapTime;
    }

    public override void ExitState(WeaponContext context)
    {
        
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        base.ManageFlashlightDrain(context);

        if (Time.time > timeToFinishSwap)
        {
            context.currentWeapon.audioSource.PlayOneShot(context.currentWeapon.unholsterSound, 0.5f * PlayerManager.instance.soundMultiplier);
            return context.idleState;
        }

        return this;
    }
}
