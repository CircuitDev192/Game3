using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContext : Context<WeaponContext>
{
    #region Fields

    #region Weapon States

    public WeaponIdleState idleState = new WeaponIdleState();
    public WeaponFireState fireState = new WeaponFireState();
    public WeaponReloadState reloadState = new WeaponReloadState();
    public WeaponSwapState swapState = new WeaponSwapState();

    #endregion

    public Animator playerAnimator;
    public Transform weaponRoot;
    public WeaponBase[] weaponPrefabs;
    public WeaponBase[] consumablePrefabs;
    public string equippedConsumable;
    public List<WeaponBase> weapons;
    public List<WeaponBase> consumables;
    public WeaponBase currentWeapon;
    public int currentWeaponIndex;
    public int currentConsumableIndex;
    public bool flashlightOn = false;
    public bool consumableEquipped = false;
    private bool isInPickupRange = false;

    public float weaponSwapTime;

    public float currentScrollDelta;

    public Camera mainCamera;

    public GameState gameState;

    #endregion

    public override void InitializeContext()
    {
        mainCamera = Camera.main;

        currentState = idleState;
        currentState.EnterState(this);

        foreach (WeaponBase weapon in weaponPrefabs)
        {
            if (weapon.name == "AK-47" || weapon.name == "Deagle" || weapon.name == "Katana")
            {
                WeaponBase weap = Instantiate(weapon, weaponRoot);
                weap.enabled = false;
                weapons.Add(weap);
            }
        }

        foreach (WeaponBase consumable in consumablePrefabs)
        {
            WeaponBase consume = Instantiate(consumable, weaponRoot);
            consume.enabled = false;
            consumables.Add(consume);
        }

        currentWeaponIndex = 0;
        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.enabled = true;

        currentConsumableIndex = 0;

        EventManager.GameStateChanged += GameStateChanged;
        EventManager.PlayerCollidedWithPickup += PlayerCollidedWithPickup;
        //EventManager.PlayerChangedConsumable += PlayerChangedConsumable;
    }

    private void PlayerChangedConsumable(string consumableName)
    {
        equippedConsumable = consumableName;
    }

    private void PlayerCollidedWithPickup(string weaponName, bool isConsumable)
    {
        isInPickupRange = true;
        EventManager.PlayerLeftPickup += PlayerLeftPickup;
        StartCoroutine(WaitForPlayerToPickupWeapon(weaponName, isConsumable));
    }

    private void PlayerLeftPickup()
    {
        isInPickupRange = false;
        EventManager.PlayerLeftPickup -= PlayerLeftPickup;
    }

    private IEnumerator WaitForPlayerToPickupWeapon(string weaponName, bool isConsumable)
    {
        while (!Input.GetKeyDown(KeyCode.E) && isInPickupRange)
        {
            yield return null;
        }
        if (isInPickupRange)
        {
            if (isConsumable)
            {
                foreach (WeaponBase consumable in consumables)
                {
                    if (consumable.name == weaponName)
                    {
                        consumable.totalAmmo++;
                        if (currentWeapon.name == weaponName)
                        {
                            EventManager.TriggerTotalAmmoChanged(consumable.totalAmmo);
                        }
                        EventManager.TriggerPlayerPickedUpWeapon(consumable.name, true);
                    }
                }
            }
            else
            {
                foreach (WeaponBase weapon in weaponPrefabs)
                {
                    if (weapon.name == weaponName)
                    {
                        EventManager.TriggerPlayerPickedUpWeapon(weapons[weapon.weaponTypeInt].name, false);
                        Destroy(weapons[weapon.weaponTypeInt].gameObject);
                        WeaponBase weap = Instantiate(weapon, weaponRoot);
                        weapons[weapon.weaponTypeInt] = weap;

                        if (currentWeaponIndex != weapon.weaponTypeInt)
                        {
                            weap.enabled = false;
                        }
                        else
                        {
                            currentState.ExitState(this);
                            currentState = swapState;
                            currentState.EnterState(this);
                        }
                        break;
                    }
                }
            }
        }
    }

    private void GameStateChanged(GameState gameState)
    {
        this.gameState = gameState;
    }
}
