using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public PlayerManager.AmmoType ammoType; // Must match the name variable of the ammo prefab exactly
    [SerializeField]
    private int ammoPickedUp = 0;
    [SerializeField]
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            EventManager.TriggerPlayerCollidedWithAmmo(ammoType,ammoPickedUp);
            EventManager.PlayerPickedUpAmmo += PlayerPickedUpAmmo;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            EventManager.PlayerPickedUpAmmo -= PlayerPickedUpAmmo;
            EventManager.TriggerPlayerLeftPickup();
        }
    }

    private void OnDestroy()
    {
        if (playerInRange)
        {
            EventManager.PlayerPickedUpAmmo -= PlayerPickedUpAmmo;
            EventManager.TriggerPlayerLeftPickup();
        }
    }

    void PlayerPickedUpAmmo(PlayerManager.AmmoType ammoType, int addedAmmo)
    {
        EventManager.PlayerPickedUpAmmo -= PlayerPickedUpAmmo;
        Destroy(this.gameObject);
    }
}
