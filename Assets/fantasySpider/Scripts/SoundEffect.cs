using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Taunt()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Debug.Log("taunt played");
    }
}
