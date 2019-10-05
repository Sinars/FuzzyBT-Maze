using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameWallController : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameManagement.GM.player;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
            GameManagement.GM.levelFinished = true;
    }
}
