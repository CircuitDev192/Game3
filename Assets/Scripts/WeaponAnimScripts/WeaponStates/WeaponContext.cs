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
    public List<WeaponBase> weapons;
    public int currentWeaponIndex;
    public bool flashlightOn = false;
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

        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].enabled = true;

        EventManager.GameStateChanged += GameStateChanged;
        EventManager.PlayerCollidedWithPickup += PlayerCollidedWithPickup;
    }

    private void PlayerCollidedWithPickup(string weaponName)
    {
        isInPickupRange = true;
        EventManager.PlayerLeftPickup += PlayerLeftPickup;
        StartCoroutine(WaitForPlayerToPickupWeapon(weaponName));
    }

    private void PlayerLeftPickup()
    {
        isInPickupRange = false;
        EventManager.PlayerLeftPickup -= PlayerLeftPickup;
    }

    private IEnumerator WaitForPlayerToPickupWeapon(string weaponName)
    {
        while (!Input.GetKeyDown(KeyCode.E) && isInPickupRange)
        {
            yield return null;
        }
        if (isInPickupRange)
        {
            foreach (WeaponBase weapon in weaponPrefabs)
            {
                if (weapon.name == weaponName)
                {
                    EventManager.TriggerPlayerPickedUpWeapon(weapons[weapon.weaponTypeInt].name);

                    Destroy(weapons[weapon.weaponTypeInt].gameObject);
                    WeaponBase weap = Instantiate(weapon, weaponRoot);
                    weapons[weapon.weaponTypeInt] = weap;
                    if (currentWeaponIndex != weapon.weaponTypeInt)
                    {
                        weap.enabled = false;
                    }
                    else
                    {
                        weap.enabled = true;
                        playerAnimator.SetInteger("WeaponType_int", weapon.weaponAnimation);
                        playerAnimator.SetInteger("MeleeType_int", weapon.meleeType);
                    }
                    break;
                }
            }
        }
    }

    private void GameStateChanged(GameState gameState)
    {
        this.gameState = gameState;
    }
}
