using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour {

    public Slider slider;
    public const float maxStamina = 10;
    private float currentStamina = maxStamina;
    public float staminaRecoverRate = 0.4f;
    public float staminaDepletionRate = 0.5f;
    private SlideController staminaController;
    private Animator animator;
	// Use this for initialization
	void Start () {
        staminaController = slider.GetComponent<SlideController>();
	}

    public void LoseStamina() {
        var value = staminaDepletionRate * Time.deltaTime;
        staminaController.removeValue(value);
        currentStamina -= value;
    }

    public void RecoverStamina() {
        var value = staminaRecoverRate * Time.deltaTime;
        if (currentStamina <= maxStamina) {
            staminaController.addValue(value);
            currentStamina += value;
        }
    }

    public bool HasStamina() {
        return currentStamina > 0;
    }

    // Update is called once per frame
    void Update () {
		
	}
    
}
