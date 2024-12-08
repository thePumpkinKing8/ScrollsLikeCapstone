using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioSO", menuName = "SOs/AudioSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip Clip
    {
        get
        {
            return _clip[Random.Range(0, _clip.Length)];
        }
    }
    [SerializeField] private AudioClip[] _clip;

    [Tooltip("Assign music and SFX to their own category mixer group.")]
    public AudioMixerGroup Mixer;

    public string AudioName;
    public float Volume = 1f;
    public float Pitch { get { return Random.Range(_pitch - .1f, _pitch + .1f); } } //randomises pitch
    [SerializeField] private float _pitch = 1;
    public int Priority = 128;
    public bool Loop;
    public bool PlayOnAwake = false;

    [HideInInspector] public AudioSource PlaySource;
}