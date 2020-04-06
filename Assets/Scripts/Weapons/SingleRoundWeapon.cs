using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRoundWeapon : WeaponBase
{
    public LineRenderer lineRenderer;

    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        roundsInCurrentMag--;
        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

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

        Vector3 newDirection = (hitInfo.point - shotOrigin.position).normalized;

        // Calculate the random position of the bullet trail
        float trailStartOffsetDistance = Random.Range(0, distance - distance / 4f);
        Vector3 trailStart = shotOrigin.position + newDirection * trailStartOffsetDistance;

        float trailEndOffsetDistance = Random.Range(distance / 4f, Mathf.Min(distance / 2f, distance - trailStartOffsetDistance));
        Vector3 trailEnd = trailStart + newDirection * trailEndOffsetDistance;

        lineRenderer.SetPosition(0, trailStart);
        lineRenderer.SetPosition(1, trailEnd);

        // Enable bullet trail, muzzle flash mesh, and muzzle flash light
        lineRenderer.enabled = true;
        muzzleFlashRenderer.enabled = true;
        muzzleFlashLight.enabled = true;

        audioSource.PlayOneShot(audioClips[0], 0.2f);
        //weaponSoundSync.PlaySound(0);

        // wait one frame
        yield return new WaitForEndOfFrame();

        // Disable bullet trail, muzzle flash mesh, and muzzle flash light
        lineRenderer.enabled = false;
        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;
    }

    public override void ToggleFireMode()
    {
        
    }

    protected override void OnEnable()
    {
        weaponRenderer.enabled = true;
        opticRenderer.enabled = true;
        lineRenderer.enabled = false;
        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;
        flashLight.enabled = flashlightOn;
        flashlightRenderer.enabled = true;

        EventManager.TriggerWeaponChanged(name);
        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);
    }

    protected override void OnDisable()
    {
        weaponRenderer.enabled = false;
        opticRenderer.enabled = false;
        lineRenderer.enabled = false;
        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;
        flashLight.enabled = false;
        flashlightRenderer.enabled = false;
    }
}