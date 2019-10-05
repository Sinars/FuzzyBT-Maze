using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrapTextControler : MonoBehaviour {

    private TMP_Text text;
    public float smoothApparition = 0.5f;
    public int TrapsCount { get; set; }
    // Use this for initialization
	void Start () {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update () {
        text.text = "Traps Left: " + TrapsCount;
		if (!text.text.Equals(""))
        {
            Color trapColorActive = text.color;
            trapColorActive.a = 255;
            Color newColor = Color.Lerp(text.color, trapColorActive, smoothApparition * Time.deltaTime);
            text.color = newColor;
        }
    }
}
