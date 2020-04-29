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
    private int maxAssaultRifleAmmo;
    private int maxShotgunAmmo;
    private int maxPistolAmmo;
    private int maxFragGrenades;
    private int maxFlashbangGrenades;
    private int maxMedkits;
    private int maxFlares;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flashbangEarRinging;
    [SerializeField] private AudioClip lastMissionMusic;
    [SerializeField] private AudioClip deathMusic;
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
        EventManager.PlayerKilled += PlayerKilled;

        //Max values are set to what the player spawns with. 
        //Set the max values by changing the 'current' values in the inspector
        maxAssaultRifleAmmo = assaultRifleAmmo;
        maxShotgunAmmo = shotgunAmmo;
        maxPistolAmmo = pistolAmmo;
        maxFragGrenades = fragGrenades;
        maxFlashbangGrenades = flashbangGrenades;
        maxMedkits = medkits;
        maxFlares = flares;
    }

    private void PlayerKilled()
    {
        audioSource.PlayOneShot(deathMusic);
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
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(assaultRifleAmmo + addedAmmo, 0, maxAssaultRifleAmmo), ammoType);
                break;
            case AmmoType.SHOTGUN:
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(shotgunAmmo + addedAmmo, 0, maxShotgunAmmo), ammoType);
                break;
            case AmmoType.PISTOL:
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(pistolAmmo + addedAmmo, 0, maxPistolAmmo), ammoType);
                break;
            case AmmoType.FRAG:
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(fragGrenades + addedAmmo, 0, maxFragGrenades), ammoType);
                break;
            case AmmoType.FLASH:
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(flashbangGrenades + addedAmmo, 0, maxFlashbangGrenades), ammoType);
                break;
            case AmmoType.MEDKITS:
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(medkits + addedAmmo, 0, maxMedkits), ammoType);
                break;
            case AmmoType.FLARES:
                EventManager.TriggerTotalAmmoChanged(Mathf.Clamp(flares + addedAmmo, 0, maxFlares), ammoType);
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

    public int GetMaxAmmoOfType(AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.ASSAULTRIFLE:
                return maxAssaultRifleAmmo;
            case AmmoType.SHOTGUN:
                return maxShotgunAmmo;
            case AmmoType.PISTOL:
                return maxPistolAmmo;
            case AmmoType.FRAG:
                return maxFragGrenades;
            case AmmoType.FLASH:
                return maxFlashbangGrenades;
            case AmmoType.MEDKITS:
                return maxMedkits;
            case AmmoType.FLARES:
                return maxFlares;
            case AmmoType.SUPPRESSOR:
                return 2;
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
        int currentAmmo = player.GetComponent<WeaponController>().currentWeapon.roundsInCurrentMag;
        
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
                EventManager.TriggerUpdateItemCountUI("Frag Grenade", updatedAmmo + currentAmmo);
                break;
            case AmmoType.FLASH:
                flashbangGrenades = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Flashbang", updatedAmmo + currentAmmo);
                break;
            case AmmoType.MEDKITS:
                medkits = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Medkit", updatedAmmo + currentAmmo);
                break;
            case AmmoType.FLARES:
                flares = updatedAmmo;
                EventManager.TriggerUpdateItemCountUI("Flare", updatedAmmo + currentAmmo);
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

    private void OnDestroy()
    {
        EventManager.PlayerHealthChanged -= PlayerHealthChanged;
        EventManager.TotalAmmoChanged -= TotalAmmoChanged;
        EventManager.TotalAmmoChangedSwap -= TotalAmmoChangedSwap;
        EventManager.PlayerPickedUpAmmo -= PlayerPickedUpAmmo;
        EventManager.PlayerPickedUpSuppressor -= PlayerPickedUpSuppressor;
        EventManager.SuppressorBroken -= SuppressorBroken;
        EventManager.FlashbangDetonated -= FlashbangDetonated;
        EventManager.PlayerEnteredMissionVehicle -= PlayerEnteredMissionVehicle;
        EventManager.PlayerKilled -= PlayerKilled;
    }
}
