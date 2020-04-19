using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : WeaponBase
{
    [SerializeField] private float healAmount;

    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

        roundsInCurrentMag--;

        EventManager.TriggerPlayerDamaged(-healAmount);

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
