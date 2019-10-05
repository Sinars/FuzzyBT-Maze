using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelperController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform parentSize = gameObject.transform.parent.GetComponent<RectTransform>();
        RectTransform childSize = gameObject.GetComponent<RectTransform>();
        childSize.sizeDelta = parentSize.sizeDelta;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
