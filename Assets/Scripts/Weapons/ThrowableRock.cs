using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableRock : WeaponBase
{
    [SerializeField]
    private GameObject rockThrown;

    public override IEnumerator Fire(Transform directionTransform)
    {
        yield return new WaitForSeconds(fireAnimationStartDelay);

        EventManager.TriggerAmmoCountChanged(roundsInCurrentMag);

        //instantiate grenade
        GameObject rock = Instantiate(rockThrown, this.gameObject.transform);
        rock.transform.parent = null;
        //disable renderer
        //weaponRenderer.enabled = false;


        rock.GetComponent<Rigidbody>().AddForce(directionTransform.forward.normalized * 2000);

        yield return new WaitForSeconds(0.2f);
        rock.GetComponent<MeshCollider>().enabled = true;

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
