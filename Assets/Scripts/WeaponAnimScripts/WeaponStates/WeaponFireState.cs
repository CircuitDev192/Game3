using System;
using UnityEngine;

public class WeaponFireState : WeaponBaseState
{
    private float nextShotTime = 0;

    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered fire state.");

        EventManager.TriggerSoundGenerated(context.transform.position, context.weapons[context.currentWeaponIndex].audibleDistance);

        context.playerAnimator.SetBool("Shoot_b", true);
        nextShotTime = (nextShotTime > Time.time) ? nextShotTime : Time.time;
    }

    public override void ExitState(WeaponContext context)
    {
        Debug.Log("Weapon exited fire state.");

        context.playerAnimator.SetBool("Shoot_b", false);
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        
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
            nextShotTime = Time.time + context.currentWeapon.fireRate;
        }
        // No ammo in mag, reload
        else return context.reloadState;

        return this;
    }
}
