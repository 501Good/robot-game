using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private AudioSourcePool audio;

    void Start()
    {
        audio = FindObjectOfType<AudioSourcePool>();
        audio.Play("Background");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
