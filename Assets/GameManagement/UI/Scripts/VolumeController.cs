using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {

    public AudioMixer mixer;
    private Slider slider;
    private float initialValue;
    // Use this for initialization
	void Awake () {
        slider = GetComponent<Slider>();
        float audioValue;
        if (mixer.GetFloat("masterVolume", out audioValue))
            slider.value = audioValue;
        else
            Debug.Log("Unable to load volume");
        slider.onValueChanged.AddListener(delegate { ValueChange(); });
        initialValue = audioValue;
    }

    public float GetInitialVolume() {
        slider.value = initialValue;
        return initialValue;
    }

    private void ValueChange() {
        mixer.SetFloat("masterVolume", slider.value);
    }
    
    public void UpdateInitialValue() {
        initialValue = slider.value;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
