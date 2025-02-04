﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {

    private Transform top;
    private ParticleSystem system;
    private bool playerAround;
    private bool isOpen;
    private float rotationSpeed = 10f;
    private float maxRotation = 90f;
    private float elapsedRotation;
    public float healDelay = 0.5f;
    private bool emitted = false;
    public float healAmount;
    private PlayerHealth playerHealth;
    // Use this for initialization
    void Start() {
        top = gameObject.transform.parent.GetChild(0);
        system = gameObject.GetComponentInChildren<ParticleSystem>();
        elapsedRotation = 0;
        playerHealth = GameManagement.GM.player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        if (!isOpen) {
            if (Input.GetKey(KeyCode.E) && playerAround) {
                UIManagement.UI.ActivateButton();
                Vector3 initialRotation = top.transform.eulerAngles;
                if (elapsedRotation < maxRotation) {
                    Vector3 newRotation = new Vector3(initialRotation.x, initialRotation.y, initialRotation.z - rotationSpeed);
                    Vector3 smoothRotation = Vector3.LerpUnclamped(top.eulerAngles, newRotation, rotationSpeed * Time.deltaTime);
                    float difference = smoothRotation.z - initialRotation.z;
                    elapsedRotation += Math.Abs(difference);
                    top.eulerAngles = smoothRotation;
                }
                else {
                    isOpen = true;
                    playerHealth.Heal(healAmount);
                }
                if (!emitted) {
                    system.Play();
                    emitted = true;
                }
            }
        }

    }


    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.Equals(GameManagement.GM.player)) {
            playerAround = true;
            if (isOpen)
                UIManagement.UI.SetEnabledStatus(false);
            else
                UIManagement.UI.SetEnabledStatus(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.Equals(GameManagement.GM.player)) {
            playerAround = false;
            UIManagement.UI.SetEnabledStatus(false);
        }
    }
}