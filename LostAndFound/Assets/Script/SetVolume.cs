using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioManager musicMixer;
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        musicMixer = GameObject.FindGameObjectWithTag("Respawn").GetComponent<AudioManager>();

    }
    public void SetLevel(float sliderValue)
    {
        sliderValue = slider.value;
        musicMixer.volume = (sliderValue);
    }
}