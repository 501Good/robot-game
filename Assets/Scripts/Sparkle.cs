using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    public ParticleSystem[] Sparks;
    
    public void CreateSpark(int idx)
    {
        Sparks[idx].Play();
    }
}
