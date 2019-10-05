using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageControl : MonoBehaviour {

    public float spiderDamage = 2;

    public float poisonDamage = 1.0f;

    private bool playerHit;

    private float passedTime;

    private readonly float smooth = 0.5f;

    public float poisonSpan = 1f; // seconds

    public float attackRate = 0.5f;

    public float dmgReceivedTimer = 0;

    private Slider slider;
	// Use this for initialization
	void Start () {
        slider = GameObject.FindGameObjectWithTag("LifeSlider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHit)
        {
            passedTime = Time.time;
            dmgReceivedTimer = dmgReceivedTimer == 0 ? Time.time : dmgReceivedTimer;

            if (Time.time > (dmgReceivedTimer + attackRate))
            {

                slider.value -= spiderDamage;
                dmgReceivedTimer = Time.time;

            }
            if (Time.time < (passedTime + poisonSpan))
            {

                float newSliderValue = Mathf.Lerp(slider.value, slider.value - poisonDamage, smooth * Time.deltaTime);
                slider.value = newSliderValue;

                //Debug.Log(slider.value);
            }
            if (slider.value == 0)
            {
                GameManagement.GM.playerDead = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Spider"))
            playerHit = false;
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Spider"))
        {
            playerHit = true;
        }
    }
}
