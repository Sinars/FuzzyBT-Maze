using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeverControler : MonoBehaviour {

    private GameObject handle;
    public float smooth = 0.5f;
    public float minYPos = 1.6f;
    private readonly float removingPos = 1.652f;
    public float loweringSpeed = 0.1f;
    public float maxYPos = 1.8f;
    private bool playerAround = false;
    private List<GameObject> triggers;
    private GameObject player;
    private bool disabled = false;
   
    // Use this for initialization
	void Start () {
        handle = gameObject.transform.GetChild(0).gameObject;
        UIManagement.UI.SetEnabledStatus(false);
        player = GameManagement.GM.player;
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Trigger");
        triggers = new List<GameObject>(temp);
        triggers.RemoveAll(trigger => !trigger.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerAround && !disabled)
        {
            if (!ManagerAudio.instance.isPlaying("Lever"))
            {
                ManagerAudio.instance.Play("Lever");
            }
            if (handle.transform.position.y > minYPos)
            {
                Vector3 updatedPosition = new Vector3(handle.transform.position.x, handle.transform.position.y - loweringSpeed, handle.transform.position.z);
                handle.transform.position = Vector3.Lerp(handle.transform.position, updatedPosition, Time.deltaTime * smooth);
            }
            if (handle.transform.position.y < removingPos)
            {
                
                if (triggers.Count > 0)
                {
                    Destroy(triggers[triggers.FindIndex(trigger=>trigger != null)]);
                    
                    disabled = true;
                }
            }
        }
        else
        if (!Input.GetKey(KeyCode.E) && playerAround && !disabled)
        {
            if (handle.transform.position.y < maxYPos && !disabled)
            {
                //Debug.Log("Rise: " + handle.transform.position.y);
                Vector3 updatedPosition = new Vector3(handle.transform.position.x, handle.transform.position.y + loweringSpeed, handle.transform.position.z);
                handle.transform.position = Vector3.Lerp(handle.transform.position, updatedPosition, Time.deltaTime * smooth);
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player) && !disabled)
        {
            UIManagement.UI.SetEnabledStatus(true);
            playerAround = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            UIManagement.UI.SetEnabledStatus(false);
            playerAround = false;

        }
    }
}
