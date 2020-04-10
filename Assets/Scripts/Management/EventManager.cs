using System;
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

    // Mission changed event
    public static Action<string, string> MissionChanged;
    public static void TriggerMissionChanged(string missionTitle, string missionDescription) { MissionChanged?.Invoke(missionTitle, missionDescription); }

    #endregion

    #region Player Info Events

    // Player health changed event
    public static Action<float> PlayerHealthChanged;
    public static void TriggerPlayerHealthChanged(float health) { PlayerHealthChanged?.Invoke(health); }

    // Player killed event
    public static Action PlayerKilled;
    public static void TriggerPlayerKilled() { PlayerKilled?.Invoke(); }

    // Weapon ammo count changed event
    public static Action<int> AmmoCountChanged;
    public static void TriggerAmmoCountChanged(int ammoCount) { AmmoCountChanged?.Invoke(ammoCount); }

    // Total ammo count changed event
    public static Action<int> TotalAmmoChanged;
    public static void TriggerTotalAmmoChanged(int totalAmmo) { TotalAmmoChanged?.Invoke(totalAmmo); }

    // Weapon changed event
    public static Action<string> WeaponChanged;
    public static void TriggerWeaponChanged(string weapon) { WeaponChanged?.Invoke(weapon); }

    // Flashlight power changed event
    public static Action<float> FlashLightPowerChanged;
    public static void TriggerFlashLightPowerChanged(float power) { FlashLightPowerChanged?.Invoke(power); }

    // Suppressor durability changed event
    public static Action<float> SuppressorDurabilityChanged;
    public static void TriggerSuppressorDurabilityChanged(float durability) { SuppressorDurabilityChanged?.Invoke(durability); }

    // Player walks into weapon pickup
    public static Action<string> PlayerCollidedWithPickup;
    public static void TriggerPlayerCollidedWithPickup(string weaponName) { PlayerCollidedWithPickup?.Invoke(weaponName); }

    // Player walks away from weapon pickup
    public static Action PlayerLeftPickup;
    public static void TriggerPlayerLeftPickup() { PlayerLeftPickup?.Invoke(); }

    // Player picked up a weapon
    public static Action<string> PlayerPickedUpWeapon;
    public static void TriggerPlayerPickedUpWeapon(string previousWeaponName) { PlayerPickedUpWeapon?.Invoke(previousWeaponName); }

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
