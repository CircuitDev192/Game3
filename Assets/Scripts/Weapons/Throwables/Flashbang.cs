using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    [SerializeField]
    private float timer = 5f;
    private bool isExploded = false;
    [SerializeField]
    private GameObject flashbangLight;
    [SerializeField]
    private GameObject managedLight;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip explosionSound;
    [SerializeField]
    private AudioClip[] collisionSounds;
    [SerializeField]
    private float collisionAudibleDistance;
    [SerializeField]
    private float explosionAudibleDistance;
    [SerializeField]
    private float stunDistance;
    private List<GameObject> targets = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !isExploded)
        {
            EventManager.TriggerFlashbangDetonated(this.transform.position, stunDistance);
            StartCoroutine(FlashBangLightEffect());
            foreach (GameObject target in targets)
            {
                //set to stun state
            }

            audioSource.pitch = 1f;
            audioSource.maxDistance = explosionAudibleDistance;
            audioSource.PlayOneShot(explosionSound, 1f * PlayerManager.instance.soundMultiplier);
            EventManager.TriggerSoundGenerated(this.transform.position, explosionAudibleDistance);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            isExploded = true;
            Destroy(this.gameObject, 4f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            audioSource.pitch = Random.Range(0.75f, 1.25f);
            audioSource.maxDistance = collisionAudibleDistance * 2;
            audioSource.PlayOneShot(collisionSounds[Random.Range(0, collisionSounds.Length)], 0.25f * PlayerManager.instance.soundMultiplier);
            EventManager.TriggerSoundGenerated(this.transform.position, collisionAudibleDistance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageAble script = other.gameObject.GetComponentInParent<IDamageAble>();
        if (script != null)
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageAble script = other.transform.gameObject.GetComponent<IDamageAble>();
        if (script != null)
        {
            targets.Remove(other.gameObject);
        }
    }

    IEnumerator FlashBangLightEffect()
    {
        flashbangLight.SetActive(true);
        managedLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        flashbangLight.SetActive(false);
        managedLight.SetActive(false);
    }
}
