using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour {

    // Use this for initialization

    public bool playerDead;

    public bool levelFinished;

    public static GameManagement GM;
    

    public void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    public GameObject player;

}
