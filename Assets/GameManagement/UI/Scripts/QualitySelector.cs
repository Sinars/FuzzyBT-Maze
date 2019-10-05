using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySelector : MonoBehaviour {

    private TMP_Dropdown dropdown;
    private int initialValue;
	// Use this for initialization
	void Start () {
        initialValue = QualitySettings.GetQualityLevel();
        dropdown = GetComponent<TMP_Dropdown>();
        foreach (string s in QualitySettings.names)
            dropdown.options.Add(new TMP_Dropdown.OptionData(s.ToUpper()));
        dropdown.value = QualitySettings.GetQualityLevel();
        dropdown.onValueChanged.AddListener(delegate { ChangeValue(); });
    }
	
    private void ChangeValue() {
        QualitySettings.SetQualityLevel(dropdown.value);
    }
    

    public int GetInitialQuality() {
        dropdown.value = initialValue;
        return initialValue;
    }

    public void UpdateInitialQuality() {
        initialValue = QualitySettings.GetQualityLevel();
    }



    // Update is called once per frame
    void Update () {
	}
}
