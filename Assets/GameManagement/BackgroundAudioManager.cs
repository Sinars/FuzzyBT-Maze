using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudioManager : MonoBehaviour {

    private float spentTime;
    private float mixingRate = 70f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > (spentTime + mixingRate))
        {
            spentTime = Time.time;
            ManagerAudio.instance.PlayTheme("Moan");
        }
	}
}
