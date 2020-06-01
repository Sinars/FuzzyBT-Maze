using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Constants;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using static Constants.Keyword;

public class PlayerController : MonoBehaviour {

    public bool Rooted { get; set; }

    public float walkSpeed = 2;
    public float runSpeed = 5;
    public float turnSpeed;
    public float speedSmoothTime = 0.1f;
    public float attackRange = 1.5f;
    public bool speechControlToggle;
    float speedSmoothVelocity;
    float currentSpeed;
    public Camera camera;
    private PlayerStamina playerStamina;
    private KeywordRecognizer keywordRecognizer;
    private DictationRecognizer dictationRecognizer;
    private long currentTime = 0;

    private List<GameObject> levers;
    private List<GameObject> chests;

    private bool timeActionTriggered = false;

    // keep track of last match keyword
    private string lastMatchedKeyword = "";
    private string previouslyLastMatched = "";

    Animator animator;
    AudioSource walkSound;
    AudioSource runSound;

    // Use this for initialization
    void Start ()
    {
        levers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Lever"));
        chests = new List<GameObject>(GameObject.FindGameObjectsWithTag("Chest"));
        Debug.Log("Levers: " + levers.Count);
        currentTime = Environment.TickCount;
        Rooted = false;

        animator = GetComponent<Animator>();
           
        playerStamina = GetComponent<PlayerStamina>();
        keywordRecognizer = new KeywordRecognizer(Keyword.Keys, ConfidenceLevel.Low);
        dictationRecognizer = new DictationRecognizer();
//        dictationRecognizer.Start();
        keywordRecognizer.Start();
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;

    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        Debug.Log(text);
        throw new NotImplementedException();
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
        animator.SetBool("attack", false);
    }



    Boolean detectAttack()
    {
        // check if left click is pressed
        return Input.GetKey(KeyCode.V);
    }


    //Update is called once per frame
    void Update() {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.red);

        if (Rooted) return;
        if (speechControlToggle)
        {
            long turnTime = 300;
            // for 90 degree turn


            if (!lastMatchedKeyword.Equals(Run))
            {
                playerStamina.RecoverStamina();
            }
            switch (lastMatchedKeyword)
            {
                case Run:
                    if (playerStamina.HasStamina())
                    {
                        playerStamina.LoseStamina();
                        currentSpeed = Mathf.SmoothDamp(currentSpeed, runSpeed, ref speedSmoothVelocity, speedSmoothTime);

                        animator.SetFloat("posY", 1, speedSmoothTime, Time.deltaTime);
                        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
                    }
                    break;
                case MoveForward:
                case GoForward:
                    ForwardMovement();
                    break;
                case TurnLeft:
                case GoLeft:
                    Turn(turnTime, "left");

                    break;
                case TurnRight:
                case GoRight:
                    Turn(turnTime, "right");

                    break;
                case StopMoving:
                case Stop:
                case StopRunning:
                    ResetPosition();
                    break;
                case Pull:
                    InteractableObjectAction(GetClosestObject,  levers, "pullLever",true);
                    break;
                case TurnAround:
                    transform.Rotate(Vector3.up * 180);
                    lastMatchedKeyword = Stop;
                    break;
                case StepBack:
                    if (!timeActionTriggered)
                    {
                        currentTime = Environment.TickCount;
                        timeActionTriggered = true;
                    }
                    transform.Translate(transform.forward * (- currentSpeed) * Time.deltaTime, Space.World);
                    animator.SetFloat("posY", -0.5f, speedSmoothTime, Time.deltaTime);
                    if (Environment.TickCount - currentTime > turnTime)
                    {
                        timeActionTriggered = false;
                        lastMatchedKeyword = Stop;
                    }
                    break;
                case GoToLever:
                    InteractableObjectAction(GetClosestObject, levers,"pullLever");
                    break;
                case Kick:
                    animator.SetBool("attack", true);
                    lastMatchedKeyword = Stop;
                    break;
                case GoToChest:
                    InteractableObjectAction(GetClosestObject, chests, "openChest");
                    break;
                case OpenChest:
                    InteractableObjectAction(GetClosestObject, chests, "openChest", true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            bool running = Input.GetKey(KeyCode.LeftShift);
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movementModifier(running, input);
            Debug.Log(input);
            // choose running speed based on axis input and left shift handler
            float targetSpeed = ((running && playerStamina.HasStamina() && input[1] >= 0) ? runSpeed :
                input[1] < 0 ? walkSpeed / 2 : walkSpeed);
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            // move the player forward based on vertical axis input
            if (input[1] != 0)
                transform.Translate(transform.forward * currentSpeed * Time.deltaTime * input[1], Space.World);

            transform.Rotate(Vector3.up, turnSpeed / 2 * input[0], Space.World);

            // choose speed percent value based on axis input

            float forward = running && playerStamina.HasStamina() ? input[1] : input[1] / 2;

            float sideWays = input[1] <= 0 ? input[0] / 2 : input[0];

            animator.SetFloat("posY", forward, speedSmoothTime, Time.deltaTime);
            animator.SetFloat("posX", sideWays, speedSmoothTime, Time.deltaTime);

            // change attacking animation based on whether the attack input was triggered or not
            animator.SetBool("attack", detectAttack());
        }
    }

    private void ForwardMovement()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, walkSpeed, ref speedSmoothVelocity, speedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        animator.SetFloat("posY", 0.5f, speedSmoothTime, Time.deltaTime);
        currentTime = Environment.TickCount;
    }

    private void InteractableObjectAction(Func<List<GameObject>, GameObject>  method, List<GameObject> objects, string eventName, bool interact = false)
    {
        GameObject target = method(objects);
        Vector3 screenPoint = camera.WorldToViewportPoint(target.transform.position);
        Vector3 targetPositionOnGround = new Vector3(target.transform.position.x, 0, target.transform.position.z); 
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            Debug.Log("Object Is in sight");
            animator.SetFloat("posY", 0.5f, speedSmoothTime, Time.deltaTime);
            transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));

            if (Vector3.Distance(transform.position, targetPositionOnGround) >= 1)
            {
                //move towards the player
                transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
            }
            else
            {
                ResetPosition();
                lastMatchedKeyword = Keyword.Stop;
            }

            if (interact)
            {
                EventManager.TriggerEvent(eventName);
            }
        }
        else
        {
            ResetPosition();
        }
    }

    private void Turn(long turnTime, string direction)
    {
        if (!timeActionTriggered)
        {
            currentTime = Environment.TickCount;
            timeActionTriggered = true;
        }

        
        if (direction.Equals("right"))
        {
            animator.SetFloat("posX", 1f, speedSmoothTime, Time.deltaTime);
            transform.Rotate(Vector3.up, turnSpeed / 2, Space.World);
        }
        else
        {
            animator.SetFloat("posX", -1f, speedSmoothTime, Time.deltaTime);

            transform.Rotate(Vector3.up, -turnSpeed / 2, Space.World);
        }

        if (Environment.TickCount - currentTime <= turnTime) return;
        lastMatchedKeyword = previouslyLastMatched;
        timeActionTriggered = false;
        ResetPosition();
    }


    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text + " " +args.confidence);
        previouslyLastMatched = lastMatchedKeyword;
        lastMatchedKeyword = args.text;
    }

    private GameObject GetClosestObject(List<GameObject> objects)
    {
        GameObject finalItem = objects[0];
        objects.ForEach(item =>
            {
                if (HasLesserDistance(item, finalItem, gameObject))
                {
                    finalItem = item;
                }
            }
        );
        return finalItem;
    }

    private bool HasLesserDistance(GameObject target1, GameObject target2, GameObject player)
    {
        return Vector3.Distance(target1.transform.position, player.transform.position) <
               Vector3.Distance(target2.transform.position, player.transform.position);
    }

}
