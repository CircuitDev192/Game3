using UnityEngine;

public abstract class WeaponBaseState : BaseState<WeaponContext>
{
    public void ManageFlashlightDrain(WeaponContext context)
    {
        if (!context.flashlightOn) return;

        if (context.consumableEquipped) return;

        if (context.currentWeaponIndex == 2) return; //Checks if holding melee weapon.

        context.currentFlashlightBattery = Mathf.Clamp(context.currentFlashlightBattery - context.flashlightDrainRate * Time.deltaTime, 0, 100f);

        if (context.currentFlashlightBattery == 0f)
        {
            context.flashlightDead = true;

            context.flashlightOn = false;
            context.currentWeapon.flashLight.enabled = context.flashlightOn;
            context.currentWeapon.flashlightOn = context.flashlightOn;
        }

        EventManager.TriggerFlashLightPowerChanged(context.currentFlashlightBattery);
    }
}
