using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundHandler : MonoBehaviour {

    private AudioSource step;
    private float elapsedTime;

    private float playRate = 0.1f;
    // Use this for initialization
    void Start () {
        step = GetComponent<AudioSource>();
        elapsedTime = Time.time;
       
    }
    private void OnTriggerEnter(Collider other) {
        if (Time.time > elapsedTime + playRate) {
            step.PlayOneShot(step.clip);
        }
        elapsedTime = Time.time;
    }



}
