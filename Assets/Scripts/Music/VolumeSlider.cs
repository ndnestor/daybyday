using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        MusicPlayer.Instance.SetVolume(slider.value);
    }
}
