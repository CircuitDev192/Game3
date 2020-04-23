using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        RaycastHit hitInfo;

        float distance = range;

        // Did we hit anything?
        if (Physics.Raycast(directionTransform.position, directionTransform.forward, out hitInfo))
        {
            if (hitInfo.distance < range)
            {
                // If it was a zombie
                if (hitInfo.transform.CompareTag("zombie"))
                {
                    hitInfo.transform.gameObject.GetComponentInParent<IDamageAble>().Damage(damage);
                    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce, ForceMode.Impulse);

                    GameObject effect = Instantiate(bloodSplatter, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(effect, 1f);
                }
                // If it was anything else
                else
                {
                    GameObject effect = Instantiate(impactSpark, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(effect, 1f);
                }

                distance = hitInfo.distance;
            }
        }

        audioSource.PlayOneShot(shotSound, 0.4f);

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