using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public enum AmmoType
    {
        ASSAULTRIFLE,
        SHOTGUN,
        PISTOL,
        FRAG,
        FLASH,
        MEDKITS,
        FLARES,
        INF_OR_MELEE
    }

    public GameObject player;

    #region Player Values

    [SerializeField] private float playerHealth;
    [SerializeField] private int assaultRifleAmmo;
    [SerializeField] private int shotgunAmmo;
    [SerializeField] private int pistolAmmo;
    [SerializeField] private int fragGrenades;
    [SerializeField] private int flashbangGrenades;
    [SerializeField] private int medkits;
    [SerializeField] private int flares;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventManager.PlayerHealthChanged += PlayerHealthChanged;
        EventManager.TotalAmmoChanged += TotalAmmoChanged;
        EventManager.TotalAmmoChangedSwap += TotalAmmoChangedSwap;
    }

    public int GetTotalAmmoOfType(AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.ASSAULTRIFLE:
                return assaultRifleAmmo;
            case AmmoType.SHOTGUN:
                return shotgunAmmo;
            case AmmoType.PISTOL:
                return pistolAmmo;
            case AmmoType.FRAG:
                return fragGrenades;
            case AmmoType.FLASH:
                return flashbangGrenades;
            case AmmoType.MEDKITS:
                return medkits;
            case AmmoType.FLARES:
                return flares;
            default:
                return 0;
        }
    }

    private void TotalAmmoChanged(int updatedAmmo, AmmoType ammoType)
    {
        SetAmmoValue(updatedAmmo, ammoType);
    }

    private void TotalAmmoChangedSwap(int updatedAmmo, AmmoType ammoType)
    {
        SetAmmoValue(updatedAmmo, ammoType);
    }

    private void PlayerHealthChanged(float healthDelta)
    {
        playerHealth += healthDelta;
    }

    private void SetAmmoValue(int updatedAmmo, AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.ASSAULTRIFLE:
                assaultRifleAmmo = updatedAmmo;
                break;
            case AmmoType.SHOTGUN:
                shotgunAmmo = updatedAmmo;
                break;
            case AmmoType.PISTOL:
                pistolAmmo = updatedAmmo;
                break;
            case AmmoType.FRAG:
                fragGrenades = updatedAmmo;
                break;
            case AmmoType.FLASH:
                flashbangGrenades = updatedAmmo;
                break;
            case AmmoType.MEDKITS:
                medkits = updatedAmmo;
                break;
            case AmmoType.FLARES:
                flares = updatedAmmo;
                break;
            default:
                break;
        }
    }
}
