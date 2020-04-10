using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private string weaponName; // Must match the name variable of the weapon prefab exactly
    [SerializeField]
    private GameObject _pointA, _pointB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Smooth Sin Wave Version
        transform.position = Vector3.Lerp(_pointA.transform.position, _pointB.transform.position, (Mathf.Sin(2f * Time.time) + 1.0f) / 2.0f);
        //PingPong - Harsh Start/Stop Version
        //transform.position = Vector3.Lerp(_pointA.transform.position, _pointB.transform.position, Mathf.PingPong(Time.time * 2f, 1.0f));

        this.transform.Rotate(0, 20f * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.TriggerPlayerCollidedWithPickup(weaponName);
            EventManager.PlayerPickedUpWeapon += PlayerPickedUpWeapon;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.PlayerPickedUpWeapon -= PlayerPickedUpWeapon;
            EventManager.TriggerPlayerLeftPickup();
        }
    }

    void PlayerPickedUpWeapon()
    {
        EventManager.PlayerPickedUpWeapon -= PlayerPickedUpWeapon;
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
