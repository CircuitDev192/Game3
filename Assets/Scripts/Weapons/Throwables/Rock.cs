using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] collisionSounds;
    [SerializeField] private float audibleDistance;

    private void OnCollisionEnter(Collision other)
    {
        audioSource.pitch = Random.Range(0.75f, 1.25f);
        audioSource.PlayOneShot(collisionSounds[Random.Range(0, collisionSounds.Length)], 1f * PlayerManager.instance.soundMultiplier);
        EventManager.TriggerSoundGenerated(this.transform.position, audibleDistance);
    }
}
