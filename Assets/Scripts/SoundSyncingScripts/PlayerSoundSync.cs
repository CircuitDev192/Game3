using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundSync : NetworkBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] footsteps;
    [SerializeField]
    private AudioClip landingSound;
    [SerializeField]
    private AudioClip jumpingSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootStep(int index)
    {
        if (isLocalPlayer)
        {
            CmdSendServerFootstepSound(index);
        }
    }

    [Command]
    private void CmdSendServerFootstepSound(int index)
    {
        RpcSendFootstepSoundToClients(index);
    }

    [ClientRpc]
    private void RpcSendFootstepSoundToClients(int index)
    {
        audioSource.clip = footsteps[index];
        audioSource.PlayOneShot(footsteps[index], 0.2f);
        footsteps[index] = footsteps[0];
        footsteps[0] = audioSource.clip;
    }

    public void PlayLanding()
    {
        if (isLocalPlayer)
        {
            CmdSendServerLandingSound();
        }
    }

    [Command]
    private void CmdSendServerLandingSound()
    {
        RpcSendLandingSoundToClients();
    }

    [ClientRpc]
    private void RpcSendLandingSoundToClients()
    {
        audioSource.clip = landingSound;
        audioSource.PlayOneShot(audioSource.clip, 0.2f);
    }

    public void PlayJumping()
    {
        if (isLocalPlayer)
        {
            CmdSendServerJumpingSound();
        }
    }

    [Command]
    private void CmdSendServerJumpingSound()
    {
        RpcSendJumpingSoundToClients();
    }

    [ClientRpc]
    private void RpcSendJumpingSoundToClients()
    {
        audioSource.clip = jumpingSound;
        audioSource.PlayOneShot(audioSource.clip, 0.2f);
    }
}

