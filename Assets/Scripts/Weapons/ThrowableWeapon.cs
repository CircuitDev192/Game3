using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : WeaponBase
{
    [SerializeField]
    private GameObject fragGrenadeThrown;

    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        roundsInCurrentMag--;
        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

        //instantiate grenade
        GameObject frag = Instantiate(fragGrenadeThrown, this.gameObject.transform);
        frag.transform.parent = null;
        //disable renderer
        //weaponRenderer.enabled = false;


        frag.GetComponent<Rigidbody>().AddForce(directionTransform.forward.normalized * 2000);

        yield return new WaitForSeconds(0.2f);
        frag.GetComponent<MeshCollider>().enabled = true;

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