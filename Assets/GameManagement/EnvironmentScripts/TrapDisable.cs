using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrapDisable : MonoBehaviour {

    private TrapTextControler textController;
	// Use this for initialization
	void Start () {
       textController = GameObject.FindGameObjectWithTag("PlayerHelper").GetComponent<TrapTextControler>();
        textController.TrapsCount += 1;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy() {
        textController.TrapsCount -= 1;
    }
}
