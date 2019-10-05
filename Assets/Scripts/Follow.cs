using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.5f;
   
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 updatedPos = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * smoothSpeed);
        transform.position = updatedPos;
		
	}
}
