using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBossAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip laser;
    public AudioClip death;

    public void PlayLaser()
    {
        audioSource.PlayOneShot(laser);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(death);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
