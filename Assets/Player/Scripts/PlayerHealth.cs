using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Slider slider;
    private SlideController healthControl;
    public float maxHealth = 100;
    private float prevHealth;
    private bool hit = false;
    private Animator animator;
	// Use this for initialization
	void Start () {
        CurrentHealth = maxHealth;
        healthControl = slider.GetComponent<SlideController>();
        animator = GetComponent<Animator>();
        prevHealth = CurrentHealth;
	}
	
    public void takeDamage(float value) {
        hit = true;
        animator.SetBool("hit", hit);
        healthControl.removeValue(value);
        CurrentHealth -= value;

    }

    public bool IsFullHealth { get { return CurrentHealth == maxHealth; } }

    public void Heal(float value) {
        if (value + CurrentHealth >= maxHealth) {
            CurrentHealth = maxHealth;
            healthControl.addValue(maxHealth - CurrentHealth);
        }
        else {
            CurrentHealth += value;
            healthControl.addValue(value);
        }
    }

    public float CurrentHealth { get; private set; }

    // Update is called once per frame
    void FixedUpdate () {
        GameManagement.GM.playerDead = CurrentHealth <= 0 ? true : false;
        if (CurrentHealth <= 0) {
            animator.SetBool("dead", true);
        }
    }
    private void LateUpdate() {
        if (prevHealth == CurrentHealth) {
            hit = false;
            animator.SetBool("hit", hit);
        }
        else
        prevHealth = CurrentHealth;
        //if (Input.anyKey) {
        //    GameManagement.GM.playerDead = true;
        //}
    }
}
