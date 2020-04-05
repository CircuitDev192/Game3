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

        foreach(WeaponBase weapon in weaponPrefabs)
        {
            WeaponBase weap = Instantiate(weapon, weaponRoot);
            weap.enabled = false;
            weapons.Add(weap);
        }

        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].enabled = true;

        EventManager.GameStateChanged += GameStateChanged;
    }

    private void GameStateChanged(GameState gameState)
    {
        this.gameState = gameState;
    }
}
