﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
	public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
