using System;
using Enums;
using UnityEngine;

public class SFXManager : MonoGenericSingleton<SFXManager>
{
    [SerializeField]

    public AudioSource soundEffectSource;
    public AudioSource musicSource;
    public Sounds[] sound;

    
    public void Play(SoundTypes soundType)
    {

        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            soundEffectSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Clip not found for sound type: " + soundType);
        }
    }
    
    public void PlayMusic(SoundTypes soundType)
    {

        AudioClip clip = GetSoundClip(soundType);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.Log("Clip not found for sound type: " + soundType);
        }
    }

    private AudioClip GetSoundClip(SoundTypes soundType)
    {
        Sounds sounds = Array.Find(sound, item => item.soundType == soundType);
        
        if (sounds != null)
            return sounds.soundClip;
        return null;

    }
}

[Serializable]
public class Sounds
{
   
    public SoundTypes soundType;
    public AudioClip soundClip;

    public bool b_IsMute = false;
    [Range(0f, 1f)] public float volume;
}

