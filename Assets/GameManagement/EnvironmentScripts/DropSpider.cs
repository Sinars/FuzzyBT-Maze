using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpider : MonoBehaviour {

    private bool spiderGenerated = false;
    private GameObject player;
    private BoxCollider boxCollision;
    public GameObject spider;
    public Vector3 dropOffset;
	// Use this for initialization
	void Start () {
        boxCollision = GetComponent<BoxCollider>();
        player = GameManagement.GM.player;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if (!spiderGenerated && other.gameObject.Equals(player)) {
            GameObject temp = Instantiate(spider, transform.position + dropOffset, Quaternion.identity);
            spiderGenerated = true;
            boxCollision.size = new Vector3(1, 1, 1);
            boxCollision.isTrigger = false;
            SpiderBehaviour spiderBehaviour = temp.GetComponent<SpiderBehaviour>();
            spiderBehaviour.area = gameObject;
        }
    }
}
