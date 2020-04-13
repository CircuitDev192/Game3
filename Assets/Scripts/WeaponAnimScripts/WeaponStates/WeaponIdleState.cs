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

        // Weapon should fire or reload
        if(Input.GetMouseButtonDown(0))
        {
            if (context.currentWeapon.roundsInCurrentMag > 0) return context.fireState;

            else return context.reloadState;
        }

        // Weapon should reload
        if (Input.GetKeyDown(KeyCode.R) && context.currentWeapon.roundsInCurrentMag < context.currentWeapon.roundsPerMag) return context.reloadState;


        context.currentScrollDelta = Input.GetAxisRaw("Mouse ScrollWheel");

        // Swap weapon
        if (context.currentScrollDelta != 0) return context.swapState;

        // Swap to consumable
        if (Input.GetKeyDown(KeyCode.G))
        {
            context.consumableEquipped = !context.consumableEquipped;
            context.consumables[context.currentConsumableIndex].enabled = context.consumableEquipped;
            return context.swapState;
        }

        // Enable Flashlight
        if (Input.GetKeyDown(KeyCode.F))
        {
            context.flashlightOn = !context.flashlightOn;
            context.currentWeapon.flashLight.enabled = context.flashlightOn;
            context.currentWeapon.flashlightOn = context.flashlightOn;
        }

        return this;
    }
}
