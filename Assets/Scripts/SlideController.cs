using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour {

    private float prevValue;
    Slider slider;
    GameObject fill;
    Image fillImage;
    public Color active;
    public Color inactive;
    public float smooth = 0.5f;
    // Use this for initialization
    void Start()
    {
        slider = GetComponent<Slider>();
        fill = slider.transform.GetChild(1).GetChild(0).gameObject;
        fillImage = fill.GetComponent<Image>();
        prevValue = slider.value;
     
    }

    public void setMaxValue(float value) {
        slider.maxValue = value;
    }
    private void Update()
    {
        if (prevValue == slider.value)
        {
            Color fillColor = Color.Lerp(fillImage.color, inactive, smooth * Time.deltaTime);
            fillImage.color = fillColor;
            
        }
        else
        {
            Color fillColor = Color.Lerp(fillImage.color, active, smooth * Time.deltaTime);
            fillImage.color = fillColor;
        }
        if (slider.value == 0)
        {
            Color color = new Color(0, 0, 0, 0);
            fillImage.color = color;
        }
    }

    public void addValue(float value) {
        slider.value += value;
    }

    public void removeValue(float value) {
        slider.value -= value;
    }

    private void LateUpdate()
    {
        prevValue = slider.value;
    }
}
