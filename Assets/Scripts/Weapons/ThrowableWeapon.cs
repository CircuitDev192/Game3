using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : WeaponBase
{
    [SerializeField]
    private GameObject grenadeThrown;

    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        roundsInCurrentMag--;

        audioSource.PlayOneShot(shotSound, 1f * PlayerManager.instance.soundMultiplier);

        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

        //instantiate grenade
        GameObject grenade = Instantiate(grenadeThrown, this.gameObject.transform);
        grenade.transform.parent = null;
        //disable renderer
        //weaponRenderer.enabled = false;


        grenade.GetComponent<Rigidbody>().AddForce(directionTransform.forward.normalized * 2000);

        yield return new WaitForSeconds(0.2f);
        grenade.GetComponent<MeshCollider>().enabled = true;

        // wait one frame
        yield return new WaitForEndOfFrame();

    }

    public override void ToggleFireMode()
    {

    }

    protected override void OnEnable()
    {
        weaponRenderer.enabled = true;

    }

    protected override void OnDisable()
    {
        weaponRenderer.enabled = false;
    }
}