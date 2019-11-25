using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource FX;

    public AudioClip HoverFX;
    public AudioClip ClickFX;

    public void HoverSound()
    {
        FX.PlayOneShot(HoverFX);
    }

    public void ClickSound()
    {
        FX.PlayOneShot(ClickFX);
    }
}
