using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
    SemiAuto,
    ThreeRoundBurst,
    Automatic
}

public abstract class WeaponBase : MonoBehaviour
{
    #region Weapon Intrinsics

    public string name;
    public float damage;
    public float fireRate;
    public int roundsPerMag;
    public float range;
    public float impactForce;
    public bool fullAuto;
    public float shotVolume;

    #endregion

    #region Animation Values

    public int weaponAnimation;
    public float fireAnimationStartDelay;
    public float reloadTime;

    public float idleBodyHorizontal = 0.6f;
    public float idleBodyVertical = 0.0f;
    public float idleHeadHorizontal = -0.8f;
    public float idleHeadVertical = 0.0f;

    public float walkBodyHorizontal = 0.6f;
    public float walkBodyVertical = 0.0f;
    public float walkHeadHorizontal = -0.8f;
    public float walkHeadVertical = 0.0f;

    public float runBodyHorizontal = 0.6f;
    public float runBodyVertical = 0.3f;
    public float runHeadHorizontal = -0.8f;
    public float runHeadVertical = 0.0f;

    #endregion

    #region Render Data

    public Light flashLight;
    public Light muzzleFlashLight;
    public Renderer muzzleFlashRenderer;
    public Transform shotOrigin;
    public GameObject impactSpark;
    public GameObject bloodSplatter;
    public Renderer weaponRenderer;
    public Renderer flashlightRenderer;

    #endregion

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public int roundsInCurrentMag;
    public int totalAmmo;
    public FireMode currentFireMode;

    public abstract IEnumerator Fire(Transform directionTransform);
    
    public void Reload()
    {
        //int roundsUsedInMag = roundsPerMag - roundsInCurrentMag;

        //if(totalAmmo < roundsUsedInMag)
        //{
        //    roundsInCurrentMag += totalAmmo;
        //    totalAmmo = 0;
        //}
        //else
        //{
        //    roundsInCurrentMag = roundsPerMag;
        //    totalAmmo -= roundsUsedInMag;
        //}

        audioSource.PlayOneShot(audioClips[1], 0.25f);
        roundsInCurrentMag = roundsPerMag;

        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);
    }

    public void SetIdleValues(Animator playerAnimator)
    {
        playerAnimator.SetFloat("Head_Horizontal_f", idleHeadHorizontal);
        playerAnimator.SetFloat("Head_Vertical_f", idleHeadVertical);

        playerAnimator.SetFloat("Body_Horizontal_f", idleBodyHorizontal);
        //playerAnimator.SetFloat("Body_Vertical_f", idleBodyVertical);

        playerAnimator.SetBool("FullAuto_b", fullAuto);
    }

    public void SetWalkValues(Animator playerAnimator)
    {
        playerAnimator.SetFloat("Head_Horizontal_f", walkHeadHorizontal);
        playerAnimator.SetFloat("Head_Vertical_f", walkHeadVertical);

        playerAnimator.SetFloat("Body_Horizontal_f", walkBodyHorizontal);
        //playerAnimator.SetFloat("Body_Vertical_f", walkBodyVertical);

        playerAnimator.SetBool("FullAuto_b", fullAuto);
    }

    public void SetRunValues(Animator playerAnimator)
    {
        playerAnimator.SetFloat("Head_Horizontal_f", runHeadHorizontal);
        playerAnimator.SetFloat("Head_Vertical_f", runHeadVertical);

        playerAnimator.SetFloat("Body_Horizontal_f", runBodyHorizontal);
        //playerAnimator.SetFloat("Body_Vertical_f", runBodyVertical);

        playerAnimator.SetBool("FullAuto_b", fullAuto);
    }

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    public abstract void ToggleFireMode();
}
