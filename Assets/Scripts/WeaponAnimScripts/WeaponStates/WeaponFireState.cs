using System;
using UnityEngine;

public class WeaponFireState : WeaponBaseState
{
    private float nextShotTime = 0;

    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered fire state.");

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
        if(Time.time > nextShotTime)
        {
            WeaponBase weapon = context.weapons[context.currentWeaponIndex];

            // Make sure we have ammo
            if (weapon.roundsInCurrentMag > 0)
            {
                // Delegate firing to the weapon
                context.StartCoroutine(weapon.Fire(context.mainCamera.transform));
                nextShotTime = Time.time + weapon.fireRate;
            }
            // No ammo in mag, reload
            else return context.reloadState;
        }

        if (Input.GetMouseButtonUp(0)) return context.idleState;

        return this;
    }
}
