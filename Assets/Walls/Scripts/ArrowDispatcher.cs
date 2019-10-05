using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDispatcher : MonoBehaviour {

    public GameObject arrow;
    private GameObject player;
    private AudioSource audioSource;

    //arrow generation position boundries

    public Vector3 upperBoundries = new Vector3(0.0f, 2.0f, 0.5f);
    public Vector3 lowerBoundries = new Vector3(-1.0f, 1.0f, -0.5f);


    //arrow intitial fixed position
    public Vector3 position;
    private Vector3 arrowAngle = new Vector3(0, 0, 90);
    private GameObject arrowHolder;
    private bool triggered = false;
    public float fireRate = 0.3f;
    public int arrowsSpawned = 8;
    private float counter = 0.5f;
    
    // Use this for initialization
    void Start()
    {
        player = GameManagement.GM.player;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            if (!triggered)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
                triggered = true;
            }            
        }
    }
    private void CreateArrows()
    {
        arrowHolder = new GameObject();
        arrowHolder.name = "ArrowHolder";
        for (var i = 0; i < arrowsSpawned; i++)
        {
            arrowHolder.transform.parent = transform;
            //Debug.Log(arrowHolder.transform.parent.name);
            GameObject temp = Instantiate(arrow, new Vector3(transform.position.x + (arrowHolder.transform.parent.gameObject.name.Equals("horizontalTrap")? -position.x : +position.x) + Random.Range(lowerBoundries.x, upperBoundries.x), Random.Range(lowerBoundries.y, upperBoundries.y) + +position.y, transform.position.z + Random.Range(lowerBoundries.z, upperBoundries.z) + (arrowHolder.transform.parent.gameObject.name.Equals("horizontalTrap") ? 0 : position.z)), Quaternion.Euler(gameObject.transform.eulerAngles.x + arrowAngle.x, gameObject.transform.eulerAngles.y + arrowAngle.y, gameObject.transform.parent.transform.eulerAngles.z + arrowAngle.z));
            temp.transform.parent = arrowHolder.transform;
        }
        
    }
	// Update is called once per frame
	void Update () {
		if (triggered)
        {
            if (Time.time > counter)
            {

                counter = Time.time + fireRate;
                CreateArrows();
            }
        }
	}
}
