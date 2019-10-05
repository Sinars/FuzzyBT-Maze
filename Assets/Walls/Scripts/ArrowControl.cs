using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowControl : MonoBehaviour {

    public float arrowSpeed = 25.0f;
    public const float smoothSpeed = 0.5f;
    private Rigidbody rb;
    private bool wallHit = false;
    private float maxArrowSpan = 0.35f;
    private GameObject player;
    private float dmgDealt = 1.25f;
    private PlayerHealth playerHealth;
    private List<GameObject> walls;

    // Use this for initialization
	void Start () {
        walls = new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall"));
        player = GameManagement.GM.player;
        playerHealth = player.GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        //if (!ManagerAudio.instance.isPlaying("ArrowShoot"))
        ManagerAudio.instance.Play("ArrowShoot");
        
	}

    // Update is called once per frame
    void Update()
    {
        if (!wallHit)
        {
            Vector3 newPosition = transform.position + transform.up * -arrowSpeed;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothSpeed);
            rb.velocity = transform.up * -arrowSpeed;
        }
        else
        {
            if (Time.time % maxArrowSpan < 0.1)
            {
                maxArrowSpan += Time.time;
                Destroy(gameObject.transform.parent.gameObject);
                Destroy(gameObject);

            }
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.Equals(player))
        {
            if (!wallHit) {
                playerHealth.takeDamage(dmgDealt);
                //Debug.Log("player hit");
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name != gameObject.transform.parent.parent.parent.gameObject.name && walls.Contains(other.gameObject))
        {
            //Debug.Log("wallHit");
            wallHit = true;
            rb.isKinematic = true;
        }
        else
            wallHit = false;
        
    }
}
