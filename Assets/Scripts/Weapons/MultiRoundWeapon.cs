using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiRoundWeapon : WeaponBase
{
    public LineRenderer[] lineRenderers;
    public float spreadAngle;

    public override IEnumerator Fire()
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        roundsInCurrentMag--;
        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

        Vector3 angles = shotOrigin.eulerAngles;
        angles.x = 0;
        angles.z = 0;
        shotOrigin.eulerAngles = angles;

        float anglePerPellet = spreadAngle / lineRenderers.Length;
        float pelletAngleOffset = angles.y - spreadAngle / 2f;

        for(int i = 0; i < lineRenderers.Length; i++)
        {
            float angle = pelletAngleOffset + i * anglePerPellet;

            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;

            RaycastHit hitInfo;

            float distance = range;

            // Did we hit anything?
            if (Physics.Raycast(shotOrigin.position, direction, out hitInfo))
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

            LineRenderer lineRenderer = lineRenderers[i];

            // Calculate the random position of the bullet trail
            float trailStartOffsetDistance = Random.Range(0, distance - distance / 4f);
            Vector3 trailStart = shotOrigin.position + direction * trailStartOffsetDistance;

            float trailEndOffsetDistance = Random.Range(distance / 4f, Mathf.Min(distance / 2f, distance - trailStartOffsetDistance));
            Vector3 trailEnd = trailStart + direction * trailEndOffsetDistance;

            lineRenderer.SetPosition(0, trailStart);
            lineRenderer.SetPosition(1, trailEnd);
        }        

        // Enable bullet trail, muzzle flash mesh, and muzzle flash light
        foreach(LineRenderer lineRenderer in lineRenderers) lineRenderer.enabled = true;

        muzzleFlashRenderer.enabled = true;
        muzzleFlashLight.enabled = true;

        audioSource.PlayOneShot(audioClips[0], 0.2f);
        //weaponSoundSync.PlaySound(0);

        // wait one frame
        yield return new WaitForEndOfFrame();

        // Disable bullet trail, muzzle flash mesh, and muzzle flash light
        foreach (LineRenderer lineRenderer in lineRenderers) lineRenderer.enabled = false;

        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;

        //shotOrigin.eulerAngles = originalAngles;
    }

    public override void ToggleFireMode()
    {
        
    }

    protected override void OnEnable()
    {
        weaponRenderer.enabled = true;
        foreach(LineRenderer lineRenderer in lineRenderers) lineRenderer.enabled = false;

        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;
        flashLight.enabled = true;
        flashlightRenderer.enabled = true;

        EventManager.TriggerWeaponChanged(name);
        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);
    }

    protected override void OnDisable()
    {
        weaponRenderer.enabled = false;
        foreach (LineRenderer lineRenderer in lineRenderers) lineRenderer.enabled = false;

        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;
        flashLight.enabled = false;
        flashlightRenderer.enabled = false;
    }
}
