using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string weaponName; // Must match the name variable of the weapon prefab exactly
    [SerializeField]
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            EventManager.TriggerPlayerCollidedWithPickup(weaponName);
            EventManager.PlayerPickedUpWeapon += PlayerPickedUpWeapon;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            EventManager.PlayerPickedUpWeapon -= PlayerPickedUpWeapon;
            EventManager.TriggerPlayerLeftPickup();
        }
    }
    private void OnDestroy()
    {
        if (playerInRange)
        {
            EventManager.PlayerPickedUpWeapon -= PlayerPickedUpWeapon;
            EventManager.TriggerPlayerLeftPickup();
        }
    }

    void PlayerPickedUpWeapon(string previousWeaponName)
    {
        EventManager.PlayerPickedUpWeapon -= PlayerPickedUpWeapon;
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
