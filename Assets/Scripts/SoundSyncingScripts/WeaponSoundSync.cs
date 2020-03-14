using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(AudioSource))]
public class WeaponSoundSync : NetworkBehaviour
{
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        if (isLocalPlayer)
        {
            CmdSendServerSound(index);
        }
    }

    [Command]
    private void CmdSendServerSound(int index)
    {
        RpcSendSoundToClients(index);
    }

    [ClientRpc]
    private void RpcSendSoundToClients(int index)
    {
        audioSource.PlayOneShot(audioClips[index], 0.2f);
    }
}
