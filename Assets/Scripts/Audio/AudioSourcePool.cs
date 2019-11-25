using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public AudioClipGroup[] Sounds;

    public static AudioSourcePool instance;

    private bool _isPlaying = false;
    public string CurrentPlaying;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (AudioClipGroup audioClipGroup in Sounds)
        {
            audioClipGroup.AudioSource = gameObject.AddComponent<AudioSource>();
            audioClipGroup.AudioSource.clip = audioClipGroup.Clip;

            audioClipGroup.Duration = audioClipGroup.AudioSource.clip.samples / audioClipGroup.AudioSource.clip.frequency;

            audioClipGroup.AudioSource.volume = audioClipGroup.Volume;
            audioClipGroup.AudioSource.pitch = audioClipGroup.Pitch;
            audioClipGroup.AudioSource.loop = audioClipGroup.Loop;
        }
    }

    public void Play(string name)
    {
        AudioClipGroup s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        _isPlaying = true;

        s.AudioSource.Play();
        CurrentPlaying = s.Name;
        
        if (s.AudioSource.loop == false)
        {
            _isPlaying = false;
        }
    }

    public void Stop(string name)
    {
        AudioClipGroup s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        _isPlaying = false;
        s.AudioSource.Stop();
    }

    public void StopAll()
    {
        foreach (AudioClipGroup s in Sounds)
        {
            s.AudioSource.Stop();
        }
        _isPlaying = false;
    }

    public bool IsPlaying()
    {
        return _isPlaying;
    }
}
