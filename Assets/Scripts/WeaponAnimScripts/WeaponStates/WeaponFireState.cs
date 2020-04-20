using System;
using UnityEngine;

public class WeaponFireState : WeaponBaseState
{
    private float nextShotTime = 0;

    public override void EnterState(WeaponContext context)
    {
        context.playerAnimator.SetBool("Shoot_b", true);
        nextShotTime = (nextShotTime > Time.time) ? nextShotTime : Time.time;
    }

    public override void ExitState(WeaponContext context)
    {
        context.playerAnimator.SetBool("Shoot_b", false);
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        base.ManageFlashlightDrain(context);

        // We can fire another shot
        if (Time.time < nextShotTime) return this;

        if (!Input.GetMouseButton(0))
        {
            if (context.currentWeapon.roundsInCurrentMag == 0)
            {
                return context.reloadState;
            }
            else return context.idleState;
        }

        // Make sure we have ammo
        if (context.currentWeapon.roundsInCurrentMag > 0)
        {
            // Delegate firing to the weapon
            context.StartCoroutine(context.currentWeapon.Fire(context.mainCamera.transform));

            if (context.currentWeapon.equippedSuppressor)
            {
                EventManager.TriggerSoundGenerated(context.currentWeapon.transform.position, context.weapons[context.currentWeaponIndex].audibleDistance * 0.10f);
            }
            else
            {
                EventManager.TriggerSoundGenerated(context.currentWeapon.transform.position, context.weapons[context.currentWeaponIndex].audibleDistance);
            }

            nextShotTime = Time.time + context.currentWeapon.fireRate;
        }
        // No ammo in mag, reload
        else return context.reloadState;

        return this;
    }
}
