using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebControl : MonoBehaviour {

    
    private PlayerStatus playerStatus;

    private SlideController rootControl;

    public float RootTime;
    public float radioActiveDamage;
    public float rate;

    private float elapsedTime;
    // Use this for initialization
	void Start () {
        elapsedTime = Time.time;
        GameObject player = transform.parent.gameObject;
        playerStatus = player.GetComponent<PlayerStatus>();
        playerStatus.AddBuff(new Buff(Effect.ROOTED, radioActiveDamage, rate));

    }

    public void Update() {
        float valueToRemove = Time.time - elapsedTime;
        elapsedTime = Time.time;
        RootTime -= valueToRemove;
    }
    // Update is called once per frame

    private void LateUpdate() {
        if (RootTime <= 0) {
            playerStatus.NegateBuff(Effect.ROOTED);
            Destroy(gameObject);
        }
    }
}
