using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource PlayerAudioSource;

    public AudioClip Footstep;
    public AudioClip Shot;
    public AudioClip Death;

    public void PlayFootstep()
    {
        PlayerAudioSource.PlayOneShot(Footstep);
    }

    public void PlayShot()
    {
        PlayerAudioSource.PlayOneShot(Shot);
    }

    public void PlayDeath()
    {
        PlayerAudioSource.PlayOneShot(Death);
    }
}
