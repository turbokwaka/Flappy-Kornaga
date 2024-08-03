using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1f;
    [Range(.1f, 3f)] public float pitch = 1f;

    [HideInInspector] public AudioSource source;

    public bool loop;
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("theme");
    }

    public void Play(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }
    
    public void MuteSFX()
    {
        foreach (var s in sounds)
        {
            if (!s.source.loop) // Assuming non-looping sounds are SFX
            {
                s.source.mute = !s.source.mute;
            }
        }
    }

    public void MuteMusic()
    {
        foreach (var s in sounds)
        {
            if (s.source.loop) // Assuming looping sounds are background music
            {
                s.source.mute = !s.source.mute;
            }
        }
    }
}