using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    public AudioMixer _audio;
    public GameObject ON;
    public GameObject OFF;

    public void SetVolume(float vol)
    {
        _audio.SetFloat("vol", vol);
        Debug.Log("Volume set to " + vol);
    }
    public void On()
    {
        AudioListener.volume = 0;
        ON.SetActive(false);
        OFF.SetActive(true);
    }
    public void Off()
    {
        AudioListener.volume = 1;
        OFF.SetActive(false);
        ON.SetActive(true);
    }
}
