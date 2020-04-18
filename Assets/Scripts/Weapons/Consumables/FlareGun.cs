using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGun : WeaponBase
{
    [SerializeField]
    private GameObject flarePrefab;

    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        roundsInCurrentMag--;

        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

        //instantiate grenade
        GameObject flare = Instantiate(flarePrefab, this.gameObject.transform);
        flare.transform.parent = null;

        flare.GetComponent<Rigidbody>().AddForce(directionTransform.forward.normalized * 3000);

        audioSource.PlayOneShot(shotSound, 0.7f * PlayerManager.instance.soundMultiplier);

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
