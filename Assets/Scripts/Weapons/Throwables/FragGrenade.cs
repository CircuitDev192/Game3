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
    private AudioClip collisionSound;
    [SerializeField]
    private float collisionAudibleDistance;
    [SerializeField]
    private float explosionAudibleDistance;


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !isExploded)
        {
            //explode
            Instantiate(explosion, this.gameObject.transform);
            audioSource.pitch = 1f;
            audioSource.maxDistance = explosionAudibleDistance;
            audioSource.PlayOneShot(explosionSound, 1f);
            EventManager.TriggerSoundGenerated(this.transform.position, explosionAudibleDistance);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            isExploded = true;
            Destroy(this.gameObject, 4f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.maxDistance = collisionAudibleDistance * 2;
        audioSource.PlayOneShot(collisionSound, 0.15f);
        EventManager.TriggerSoundGenerated(this.transform.position, collisionAudibleDistance);
    }
}
