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
