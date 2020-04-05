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
}
