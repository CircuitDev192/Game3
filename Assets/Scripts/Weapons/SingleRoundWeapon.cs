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

        if (equippedSuppressor)
        {
            muzzleFlashRenderer.transform.position += this.transform.forward * 0.2f;
            muzzleFlashRenderer.transform.localScale /= 2f;
            muzzleFlashLight.transform.position += this.transform.forward * 0.55f;
            muzzleFlashLight.intensity /= 5f;

            // AudioSource.pitch does not affect AudioSource.PlayOneShot. Leaving it here for later though.
            audioSource.pitch = Random.Range(0.75f, 1.25f);
            audioSource.PlayOneShot(suppressedShotSound, 0.5f * PlayerManager.instance.soundMultiplier);
        }
        else
        {
            audioSource.pitch = Random.Range(0.75f, 1.25f);
            audioSource.PlayOneShot(shotSound, 0.7f * PlayerManager.instance.soundMultiplier);
        }

        audioSource.pitch = 1f;

        muzzleFlashRenderer.enabled = true;
        muzzleFlashLight.enabled = true;

        // wait one frame
        yield return new WaitForEndOfFrame();

        // Disable bullet trail, muzzle flash mesh, and muzzle flash light
        lineRenderer.enabled = false;

        if (equippedSuppressor)
        {
            muzzleFlashRenderer.transform.position -= this.transform.forward * 0.2f;
            muzzleFlashRenderer.transform.localScale *= 2f;
            muzzleFlashLight.transform.position -= this.transform.forward * 0.55f;
            muzzleFlashLight.intensity *= 5f;

            equippedSuppressor.GetComponent<Suppressor>().UpdateDurability(suppressorFatigue);
        }

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
        if (equippedSuppressor ?? false)
        {
            suppressorRenderer.enabled = true;
        } else
        {
            suppressorRenderer.enabled = false;
        }
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
        if (equippedSuppressor ?? false)
        {
            suppressorRenderer.enabled = false;
        }
    }
}