using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] AudioMixer volAudio;
    [SerializeField] GameObject ON;
    [SerializeField] GameObject OFF;
    [SerializeField] Slider volumeSlider;

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        volAudio.SetFloat("vol", volume);
   
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
