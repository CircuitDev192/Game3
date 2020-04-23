using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIPickupInfoController : MonoBehaviour
{
    [SerializeField] private Text promptText;
    [SerializeField] private Text pickupText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.PlayerCollidedWithPickup += PlayerCollidedWithPickup;
        EventManager.PlayerLeftPickup += PlayerLeftPickup;
        EventManager.PlayerCollidedWithAmmo += PlayerCollidedWithAmmo;
        EventManager.PlayerCollidedWithMissionItem += PlayerCollidedWithMissionItem;
        EventManager.PlayerLeftMissionItem += PlayerLeftMissionItem;
    }

    private void PlayerCollidedWithMissionItem(string itemName)
    {
        promptText.gameObject.SetActive(true);
        pickupText.gameObject.SetActive(true);
        pickupText.text = itemName;
    }

    private void PlayerLeftMissionItem()
    {
        promptText.gameObject.SetActive(false);
        pickupText.gameObject.SetActive(false);
    }

    private void PlayerCollidedWithAmmo(PlayerManager.AmmoType ammoType, int addedAmmo)
    {
        promptText.gameObject.SetActive(true);
        pickupText.gameObject.SetActive(true);
        pickupText.text = PlayerManager.instance.ConvertAmmoTypeToString(ammoType);
    }

    private void PlayerCollidedWithPickup(string weaponName)
    {
        promptText.gameObject.SetActive(true);
        pickupText.gameObject.SetActive(true);
        pickupText.text = weaponName;

    }

    private void PlayerLeftPickup()
    {
        promptText.gameObject.SetActive(false);
        pickupText.gameObject.SetActive(false);
    }

}
