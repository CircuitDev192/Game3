using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : MonoBehaviour
{
    [SerializeField]
    private float timer = 5f;
    private bool isExploded = false;
    [SerializeField]
    private GameObject explosion;
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
    private List<GameObject> targets = new List<GameObject>();


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !isExploded)
        {
            //explode
            Instantiate(explosion, this.transform.position, Quaternion.identity, this.transform);

            foreach (GameObject target in targets)
            {
                target.GetComponentInParent<IDamageAble>().Damage(1000f);
                target.GetComponent<Rigidbody>().AddExplosionForce(2000f, this.transform.position, this.GetComponent<SphereCollider>().radius);
            }

            audioSource.pitch = 1f;
            audioSource.maxDistance = explosionAudibleDistance;
            audioSource.PlayOneShot(explosionSound, 1f * PlayerManager.instance.soundMultiplier);
            EventManager.TriggerSoundGenerated(this.transform.position, explosionAudibleDistance);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            isExploded = true;
            Destroy(this.gameObject, 1.5f);
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
}
