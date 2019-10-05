using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WebShootController : MonoBehaviour {

    public float webSpeed;
    public GameObject web;
    public Vector3 webOffset;
    private GameObject player;
    private PlayerStatus playerStatus;
	// Use this for initialization
	void Start () {
        player = GameManagement.GM.player;
        playerStatus = player.GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
        transform.position += transform.forward * webSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        Vector3 playerPos = player.transform.position;
        if (!playerStatus.HasEffect(Effect.ROOTED)) {
            GameObject temp = GameObject.Instantiate(web, new Vector3(playerPos.x + webOffset.x, playerPos.y + webOffset.y, playerPos.z + webOffset.z), Quaternion.identity);
            temp.transform.parent = player.transform;
        }
        Destroy(gameObject);
    }
}
