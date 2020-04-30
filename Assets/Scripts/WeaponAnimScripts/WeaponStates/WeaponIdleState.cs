using UnityEngine;

public class WeaponIdleState : WeaponBaseState
{
    public override void EnterState(WeaponContext context)
    {
        
    }

    public override void ExitState(WeaponContext context)
    {
        
    }

    public override BaseState<WeaponContext> UpdateState(WeaponContext context)
    {
        if (context.gameState == GameState.Paused) return this;

        base.ManageFlashlightDrain(context);

        // Weapon should fire or reload
        if(Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (context.currentWeapon.roundsInCurrentMag > 0) return context.fireState;

            else return context.reloadState;
        }

        // Weapon should reload
        if (Input.GetKeyDown(KeyCode.R) && context.currentWeapon.roundsInCurrentMag < context.currentWeapon.roundsPerMag) return context.reloadState;


        context.currentScrollDelta = Input.GetAxisRaw("Mouse ScrollWheel");

        // Swap weapon
        if (context.currentScrollDelta != 0) return context.swapState;

        // Swap with number keys (the superior solution)
        KeyCode[] keys = {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3
        };

        for(int i = 0; i < keys.Length; i++){
            if(Input.GetKeyDown(keys[i])){
                context.currentWeaponIndex = i;
                return context.swapState;
            }
        }

        // Swap to consumable
        if (Input.GetKeyDown(KeyCode.G))
        {
            context.consumableEquipped = !context.consumableEquipped;
            context.consumables[context.currentConsumableIndex].enabled = context.consumableEquipped;
            return context.swapState;
        }

        // Equip/Unequip Suppressor
        if (Input.GetKeyDown(KeyCode.X))
        {
            //check if suppressor is equipped, if so, unequip
            if (context.currentWeapon.equippedSuppressor)
            {
                context.currentWeapon.equippedSuppressor.gameObject.GetComponent<Suppressor>().isEquipped = false;
                context.currentWeapon.equippedSuppressor = null;
                context.currentWeapon.suppressorRenderer.enabled = false;
                EventManager.TriggerSuppressorDurabilityChanged(0f);
            }
            else
            {
                bool[] suppressorAtIndexIsEquipped = { false, false };
                //if not equipped, check if a suppressor is available to equip, then do
                if (context.suppressors.Count > 0)
                {
                    foreach(GameObject suppressor in context.suppressors)
                    {
                        if (suppressor.GetComponent<Suppressor>().isEquipped)
                        {
                            suppressorAtIndexIsEquipped[context.suppressors.IndexOf(suppressor)] = true;
                        }
                    }
                    if (suppressorAtIndexIsEquipped[0] == false && suppressorAtIndexIsEquipped[1] == false)
                    {
                        float highestDur = 0f;
                        float currentDur;
                        int indexOfHighestDur = 0;
                        foreach (GameObject suppress in context.suppressors)
                        {
                            currentDur = suppress.gameObject.GetComponent<Suppressor>().GetDurability();
                            if (currentDur > highestDur)
                            {
                                highestDur = currentDur;
                                indexOfHighestDur = context.suppressors.IndexOf(suppress);
                            }
                        }
                        context.currentWeapon.equippedSuppressor = context.suppressors[indexOfHighestDur];
                        context.currentWeapon.equippedSuppressor.gameObject.GetComponent<Suppressor>().isEquipped = true;
                        context.currentWeapon.suppressorRenderer.enabled = true;
                        context.currentWeapon.equippedSuppressor.GetComponent<Suppressor>().UpdateDurability(0);
                    }
                    else if (suppressorAtIndexIsEquipped[0] == true && suppressorAtIndexIsEquipped[1] == true)
                    {
                        //Don't allow an equip
                    }
                    else
                    {
                        foreach (GameObject suppressor in context.suppressors)
                        {
                            if (suppressor.GetComponent<Suppressor>().isEquipped == false)
                            {
                                context.currentWeapon.equippedSuppressor = context.suppressors[context.suppressors.IndexOf(suppressor)];
                                context.currentWeapon.equippedSuppressor.gameObject.GetComponent<Suppressor>().isEquipped = true;
                                context.currentWeapon.suppressorRenderer.enabled = true;
                                context.currentWeapon.equippedSuppressor.GetComponent<Suppressor>().UpdateDurability(0);
                            }
                        }
                    }
                }
            }
        }

        // Enable Flashlight
        if (Input.GetKeyDown(KeyCode.F) && context.currentWeaponIndex != 2 && !context.consumableEquipped)
        {
            if (context.flashlightDead) return this;

            context.flashlightOn = !context.flashlightOn;
            context.currentWeapon.flashLight.enabled = context.flashlightOn;
            context.currentWeapon.flashlightOn = context.flashlightOn;
            if (context.flashlightOn)
            {
                context.currentWeapon.GetComponent<AudioSource>().PlayOneShot(context.currentWeapon.flashlightOnSound, 0.7f * PlayerManager.instance.soundMultiplier);
            }
            else
            {
                context.currentWeapon.GetComponent<AudioSource>().PlayOneShot(context.currentWeapon.flashlightOffSound, 0.7f * PlayerManager.instance.soundMultiplier);

            }
        }

        // Flashlight recharge
        if (Input.GetKeyDown(KeyCode.L))
        {
            context.currentFlashlightBattery = Mathf.Clamp(context.currentFlashlightBattery + context.flashlightRechargeRate, 0, 100);

            if (context.currentFlashlightBattery == 100f) context.flashlightDead = false;

            EventManager.TriggerFlashLightPowerChanged(context.currentFlashlightBattery);
        }

        return this;
    }
}
