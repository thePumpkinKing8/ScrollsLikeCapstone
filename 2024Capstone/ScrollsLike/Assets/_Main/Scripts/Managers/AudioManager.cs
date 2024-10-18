using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    // Audio clips for different actions
    [Header("Audio Clips")]
    public AudioClip walkClip;
    public AudioClip damageClip;
    public AudioClip healClip;
    public AudioClip slashClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayWalkSound()
    {
        PlaySound(walkClip);
    }

    public void PlayDamageSound()
    {
        PlaySound(damageClip);
    }

    public void PlayHealSound()
    {
        PlaySound(healClip);
    }

    public void PlaySlashSound()
    {
        PlaySound(slashClip);
    }

    // New StopSound method to stop currently playing audio
    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
