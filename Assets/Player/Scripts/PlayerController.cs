using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public bool Rooted { get; set; }

    public float walkSpeed = 2;
    public float runSpeed = 5;
    public float turnSpeed;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    private PlayerStamina playerStamina;

    Animator animator;
    AudioSource walkSound;
    AudioSource runSound;

    // Use this for initialization
    void Start () {

        Rooted = false;

        animator = GetComponent<Animator>();
           
        playerStamina = GetComponent<PlayerStamina>();
        
        

        
    }


    private void movementModifier(bool running, Vector2 inputDir)
    {
        if (running && playerStamina.HasStamina() && inputDir[1] > 0)
        {
            playerStamina.LoseStamina();

        }
        if (!running)
        {
            playerStamina.RecoverStamina();
        }
    }

    public void ResetPosition() {
        animator.SetFloat("posY", 0);
        animator.SetFloat("posX", 0);
    }


    //Update is called once per frame
    void Update() {
        if (!Rooted) {
            bool running = Input.GetKey(KeyCode.LeftShift);
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movementModifier(running, input);
            //Debug.Log("Vertical input: " + input[1] + " Has stamina: " + playerStamina.HasStamina());
            // choose running speed based on axis input and left shift handler
            float targetSpeed = ((running && playerStamina.HasStamina() && input[1] >= 0) ? runSpeed :
                                   input[1] < 0 ? walkSpeed / 2 : walkSpeed);
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            // move the player forward based on vertical axis input
            if (input[1] != 0)
                transform.Translate(transform.forward * currentSpeed * Time.deltaTime * input[1], Space.World);

            transform.Rotate(Vector3.up, turnSpeed / 2 * input[0], Space.World);

            // choose speed percent value based on axis input

            float fwd = running && playerStamina.HasStamina() ? input[1] : input[1] / 2;

            float back = input[1] <= 0 ? input[0] / 2 : input[0];

            animator.SetFloat("posY", fwd, speedSmoothTime, Time.deltaTime);
            animator.SetFloat("posX", back, speedSmoothTime, Time.deltaTime);

        }
    }

}
