﻿using System;
using UnityEngine;

public static class EventManager
{
    #region UIEvents

    // Game should resume
    public static Action UIResumeClicked;
    public static void TriggerUIResumeClicked() { UIResumeClicked?.Invoke(); }

    // Game should quit
    public static Action UIQuitClicked;
    public static void TriggerUIQuitClicked() { UIQuitClicked?.Invoke(); }

    // Show Controls Menu
    public static Action UIControlsClicked;
    public static void TriggerUIControlsClicked() { UIControlsClicked?.Invoke(); }

    // Show Controls Menu
    public static Action UIControlsBackClicked;
    public static void TriggerUIControlsBackClicked() { UIControlsBackClicked?.Invoke(); }

    // Should mouse be hidden/locked
    public static Action<bool> MouseShouldHide;
    public static void TriggerMouseShouldHide(bool shouldHide) { MouseShouldHide?.Invoke(shouldHide); }

    #endregion

    #region Game Manager Events

    // Game State Changed Event
    public static Action<GameState> GameStateChanged;
    public static void TriggerGameStateChanged(GameState gameState) { GameStateChanged?.Invoke(gameState); }

    // Scene Loaded Event
    public static Action<string> SceneLoaded;
    public static void TriggerSceneLoaded(string sceneName) { SceneLoaded?.Invoke(sceneName); }

    // Scene UnLoadedEvent
    public static Action<string> SceneUnLoaded;
    public static void TriggerSceneUnLoaded(string sceneName) { SceneUnLoaded?.Invoke(sceneName); }

    #endregion

    #region Mission Events

    // Mission info changed event
    public static Action<string, string> MissionChanged;
    public static void TriggerMissionChanged(string missionTitle, string missionDescription) { MissionChanged?.Invoke(missionTitle, missionDescription); }

    // Mission Completed Event
    public static Action MissionCompleted;
    public static void TriggerMissionCompleted() { MissionCompleted?.Invoke(); }

    // Mission Started
    public static Action StartMission;
    public static void TriggerStartMission() { StartMission?.Invoke(); }

    // Player at mission area
    public static Action PlayerAtMissionArea;
    public static void TriggerPlayerAtMissionArea() { PlayerAtMissionArea?.Invoke(); }

    // Mission Ended
    public static Action EndMission;
    public static void TriggerEndMission() { EndMission?.Invoke(); }

    // Player collided with mission item
    public static Action<string> PlayerCollidedWithMissionItem;
    public static void TriggerPlayerCollidedWithMissionItem(string itemName) { PlayerCollidedWithMissionItem?.Invoke(itemName); }

    // Player picked up mission item
    public static Action PlayerPickedUpMissionItem;
    public static void TriggerPlayerPickedUpMissionItem() { PlayerPickedUpMissionItem?.Invoke(); }

    // Player left mission item
    public static Action PlayerLeftMissionItem;
    public static void TriggerPlayerLeftMissionItem() { PlayerLeftMissionItem?.Invoke(); }

    // Game Ended
    public static Action GameEnded;
    public static void TriggerGameEnded() { GameEnded?.Invoke(); }

    #endregion

    #region Player Info Events

    // Player damaged event
    // DIFFERENT FROM HEALTH CHANGED
    //  This one triggers on damage (or healing),
    //    the other triggers once the player's new health has been calculated
    public static Action<float> PlayerDamaged;
    public static void TriggerPlayerDamaged(float damage) { PlayerDamaged?.Invoke(damage); }

    // Player health changed event
    public static Action<float> PlayerHealthChanged;
    public static void TriggerPlayerHealthChanged(float health) { PlayerHealthChanged?.Invoke(health); }

    // Player killed event
    public static Action PlayerKilled;
    public static void TriggerPlayerKilled() { PlayerKilled?.Invoke(); }

    // Flashbang Detonated
    public static Action<Vector3, float> FlashbangDetonated;
    public static void TriggerFlashbangDetonated(Vector3 flashbangPosition, float stunDistance) { FlashbangDetonated?.Invoke(flashbangPosition, stunDistance); }

    // Weapon ammo count changed event
    public static Action<int> AmmoCountChanged;
    public static void TriggerAmmoCountChanged(int ammoCount) { AmmoCountChanged?.Invoke(ammoCount); }

    // Total ammo count changed event
    public static Action<int, PlayerManager.AmmoType> TotalAmmoChanged;
    public static void TriggerTotalAmmoChanged(int totalAmmo, PlayerManager.AmmoType ammoType) { TotalAmmoChanged?.Invoke(totalAmmo, ammoType); }

    // Total ammo count updated on weapon swap event
    public static Action<int, PlayerManager.AmmoType> TotalAmmoChangedSwap;
    public static void TriggerTotalAmmoChangedSwap(int totalAmmo, PlayerManager.AmmoType ammoType) { TotalAmmoChangedSwap?.Invoke(totalAmmo, ammoType); }

    // Weapon changed event
    public static Action<string> WeaponChanged;
    public static void TriggerWeaponChanged(string weapon) { WeaponChanged?.Invoke(weapon); }

    // Flashlight power changed event
    public static Action<float> FlashLightPowerChanged;
    public static void TriggerFlashLightPowerChanged(float power) { FlashLightPowerChanged?.Invoke(power); }

    // Player picked up a suppressor
    public static Action PlayerPickedUpSuppressor;
    public static void TriggerPlayerPickedUpSuppressor() { PlayerPickedUpSuppressor?.Invoke(); }

    // Suppressor durability changed event
    public static Action<float> SuppressorDurabilityChanged;
    public static void TriggerSuppressorDurabilityChanged(float durability) { SuppressorDurabilityChanged?.Invoke(durability); }

    // Suppressor Broken
    public static Action SuppressorBroken;
    public static void TriggerSuppressorBroken() { SuppressorBroken?.Invoke(); }

    // Player walks into weapon pickup
    public static Action<string> PlayerCollidedWithPickup;
    public static void TriggerPlayerCollidedWithPickup(string weaponName) { PlayerCollidedWithPickup?.Invoke(weaponName); }

    // Player walks away from weapon pickup
    public static Action PlayerLeftPickup;
    public static void TriggerPlayerLeftPickup() { PlayerLeftPickup?.Invoke(); }

    // Player picked up a weapon
    public static Action<string> PlayerPickedUpWeapon;
    public static void TriggerPlayerPickedUpWeapon(string previousWeaponName) { PlayerPickedUpWeapon?.Invoke(previousWeaponName); }

    // Player changed the equipped consumable
    public static Action<string> PlayerChangedConsumable;
    public static void TriggerPlayerChangedConsumable(string consumableName) { PlayerChangedConsumable?.Invoke(consumableName); }

    // Player collided with ammo
    public static Action<PlayerManager.AmmoType, int> PlayerCollidedWithAmmo;
    public static void TriggerPlayerCollidedWithAmmo(PlayerManager.AmmoType ammoType, int addedAmmo) { PlayerCollidedWithAmmo?.Invoke(ammoType ,addedAmmo); }

    // Player picked up ammo
    public static Action<PlayerManager.AmmoType, int> PlayerPickedUpAmmo;
    public static void TriggerPlayerPickedUpAmmo(PlayerManager.AmmoType ammoType, int addedAmmo) { PlayerPickedUpAmmo?.Invoke(ammoType, addedAmmo); }
    
    // Update consumable count value
    public static Action<string, int> UpdateItemCountUI;
    public static void TriggerUpdateItemCountUI(string consumableName, int newValue) { UpdateItemCountUI?.Invoke(consumableName, newValue); }

    #endregion

    // Zombie killed event
    public static Action ZombieKilled;
    public static void TriggerZombieKilled() { ZombieKilled?.Invoke(); }

    // Zombie should despawn event
    public static Action<GameObject> zombieShouldDespawn;
    public static void TriggerZombieShouldDespawn(GameObject zombie) { zombieShouldDespawn?.Invoke(zombie); }    

    // Sound Generated Event
    public static Action<Vector3, float> SoundGenerated;
    public static void TriggerSoundGenerated(Vector3 location, float audibleDistance) { SoundGenerated?.Invoke(location, audibleDistance); }
}
