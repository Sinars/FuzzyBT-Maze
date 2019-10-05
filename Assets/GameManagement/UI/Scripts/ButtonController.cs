using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour {

    // Use this for initialization
    private Button button;
    private TMP_Text text;
    ColorBlock cb;
    public Color normalColor = new Color(255f, 28f, 28f, 0f);
    public Color hlColor = new Color(255f, 28f, 28f, 155f);

    void Start () {
        button = GetComponent<Button>();
        cb = button.colors;
        text = GetComponentInChildren<TMP_Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (button.IsInteractable())
        {
            text.alpha = 255;
        }
        else
            text.alpha = 0;
        if (Input.GetKey(KeyCode.E))
        {
            if (button.IsInteractable())
            {
                ChangeButtonColors(hlColor);
            }            
        }
        else
        {
            ChangeButtonColors(normalColor);
        }
	}
    private void ChangeButtonColors(Color color)
    {
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        button.colors = cb;
    }
}
