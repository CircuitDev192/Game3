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
        SUPPRESSOR,
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
    [SerializeField] private int suppressors;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flashbangEarRinging;
    [SerializeField] private AudioClip lastMissionMusic;
    [SerializeField] public float soundMultiplier = 1f;

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
        EventManager.PlayerPickedUpAmmo += PlayerPickedUpAmmo;
        EventManager.PlayerPickedUpSuppressor += PlayerPickedUpSuppressor;
        EventManager.SuppressorBroken += SuppressorBroken;
        EventManager.FlashbangDetonated += FlashbangDetonated;
        EventManager.PlayerEnteredMissionVehicle += PlayerEnteredMissionVehicle;
    }

    private void PlayerEnteredMissionVehicle()
    {
        audioSource.PlayOneShot(lastMissionMusic);
    }

    private void FlashbangDetonated(Vector3 flashbangPosition, float stunDistance)
    {
        if (Vector3.Distance(flashbangPosition, player.transform.position) < stunDistance)
        {
            audioSource.PlayOneShot(flashbangEarRinging, 0.7f);
            //Lower the volume of all sounds that use this multiplier. 
            soundMultiplier = 0.2f;
            StartCoroutine(IncreaseMultiplierBackToOne());
        }
    }

    private void PlayerPickedUpSuppressor()
    {
        SetAmmoValue(suppressors + 1, AmmoType.SUPPRESSOR);
    }

    private void SuppressorBroken()
    {
        SetAmmoValue(suppressors - 1, AmmoType.SUPPRESSOR);
    }

    private void PlayerPickedUpAmmo(AmmoType ammoType, int addedAmmo)
    {
        switch (ammoType)
        {
            case AmmoType.ASSAULTRIFLE:
                EventManager.TriggerTotalAmmoChanged(assaultRifleAmmo + addedAmmo, ammoType);
                break;
            case AmmoType.SHOTGUN:
                EventManager.TriggerTotalAmmoChanged(shotgunAmmo + addedAmmo, ammoType);
                break;
            case AmmoType.PISTOL:
                EventManager.TriggerTotalAmmoChanged(pistolAmmo + addedAmmo, ammoType);
                break;
            case AmmoType.FRAG:
                EventManager.TriggerTotalAmmoChanged(fragGrenades + addedAmmo, ammoType);
                break;
            case AmmoType.FLASH:
                EventManager.TriggerTotalAmmoChanged(flashbangGrenades + addedAmmo, ammoType);
                break;
            case AmmoType.MEDKITS:
                EventManager.TriggerTotalAmmoChanged(medkits + addedAmmo, ammoType);
                break;
            case AmmoType.FLARES:
                EventManager.TriggerTotalAmmoChanged(flares + addedAmmo, ammoType);
                break;
            default:
                break;
        }
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
            case AmmoType.SUPPRESSOR:
                return suppressors;
            default:
                return 0;
        }
    }

    public string ConvertAmmoTypeToString (AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.ASSAULTRIFLE:
                return "Assault Rifle Ammo";
            case AmmoType.SHOTGUN:
                return "Shotgun Ammo";
            case AmmoType.PISTOL:
                return "Pistol Ammo";
            case AmmoType.FRAG:
                return "Frag Grenade";
            case AmmoType.FLASH:
                return "Flashbang Grenade";
            case AmmoType.MEDKITS:
                return "Medkit";
            case AmmoType.FLARES:
                return "Flare";
            case AmmoType.SUPPRESSOR:
                return "Suppressor";
            default:
                return "";
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
                EventManager.TriggerUpdateItemCountUI("Frag Grenade", updatedAmmo + 1);
                break;
            case AmmoType.FLASH:
                flashbangGrenades = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Flashbang", updatedAmmo + 1);
                break;
            case AmmoType.MEDKITS:
                medkits = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Medkit", updatedAmmo + 1);
                break;
            case AmmoType.FLARES:
                flares = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Flare", updatedAmmo + 1);
                break;
            case AmmoType.SUPPRESSOR:
                suppressors = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Suppressor", updatedAmmo);
                break;
            default:
                break;
        }
    }

    private IEnumerator IncreaseMultiplierBackToOne()
    {
        while (soundMultiplier < 1f)
        {
            soundMultiplier += 0.05f * Time.deltaTime;
            yield return null;
        }
        soundMultiplier = 1f;
    }
}
