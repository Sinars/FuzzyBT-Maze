using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    public Slider slider;
    public RectTransform textMesh;
    public AudioMixer audioMixer;
    private VolumeController volumeController;
    private QualitySelector qualitySelector;
    
	// Use this for initialization
	void Start () {
        volumeController = slider.GetComponent<VolumeController>();
        qualitySelector = textMesh.GetComponent<QualitySelector>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate { UpdateSettings(); });
	}
	
    private void UpdateSettings() {
        audioMixer.SetFloat("masterVolume", volumeController.GetInitialVolume());
        QualitySettings.SetQualityLevel(qualitySelector.GetInitialQuality());
    }

	// Update is called once per frame
	void Update () {
		
	}
}
