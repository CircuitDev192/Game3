using UnityEngine;

public class WeaponIdleState : WeaponBaseState
{
    public override void EnterState(WeaponContext context)
    {
        Debug.Log("Weapon entered idle state.");
    }

    public override void ExitState(WeaponContext context)
    {
        Debug.Log("Weapon exited idle state.");
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        if (context.gameState == GameState.Paused) return this;

        WeaponBase weapon = context.weapons[context.currentWeaponIndex];

        // Weapon should fire or reload
        if(Input.GetMouseButtonDown(0))
        {
            if (weapon.roundsInCurrentMag > 0) return context.fireState;

            else return context.reloadState;
        }

        // Weapon should reload
        if (Input.GetKeyDown(KeyCode.R) && weapon.roundsInCurrentMag < weapon.roundsPerMag) return context.reloadState;


        context.currentScrollDelta = Input.GetAxisRaw("Mouse ScrollWheel");

        // Swap weapon
        if (context.currentScrollDelta != 0) return context.swapState;

        // Enable Flashlight
        if (Input.GetKeyDown(KeyCode.F))
        {
            context.flashlightOn = !context.flashlightOn;
            weapon.flashLight.enabled = context.flashlightOn;
            weapon.flashlightOn = context.flashlightOn;
        }

        return this;
    }
}
