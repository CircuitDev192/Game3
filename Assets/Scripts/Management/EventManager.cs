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

    // Weapon ammo count changed event
    public static Action<int> AmmoCountChanged;
    public static void TriggerAmmoCountChanged(int ammoCount) { AmmoCountChanged?.Invoke(ammoCount); }

    // Weapon changed event
    public static Action<string> WeaponChanged;
    public static void TriggerWeaponChanged(string weapon) { WeaponChanged?.Invoke(weapon); }

    // Zombie killed event
    public static Action ZombieKilled;
    public static void TriggerZombieKilled() { ZombieKilled?.Invoke(); }

    // Zombie should despawn event
    public static Action<GameObject> zombieShouldDespawn;
    public static void TriggerZombieShouldDespawn(GameObject zombie) { zombieShouldDespawn?.Invoke(zombie); }

    // Player health changed event
    public static Action<float> PlayerHealthChanged;
    public static void TriggerPlayerHealthChanged(float health) { PlayerHealthChanged?.Invoke(health); }

    // Player killed event
    public static Action PlayerKilled;
    public static void TriggerPlayerKilled() { PlayerKilled?.Invoke(); }

    // Sound Generated Event
    public static Action<Vector3, float> SoundGenerated;
    public static void TriggerSoundGenerated(Vector3 location, float audibleDistance) { SoundGenerated?.Invoke(location, audibleDistance); }
}
