using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour {

    private PlayerController controler;
    private NavMeshAgent agent;
    private GameObject[] walls;
    private PlayerHealth playerHealth;
    private PlayerStatus playerStatus;
    private GameObject wall;
    private float currentHealth;

    // Use this for initialization
    void Start () {
        playerHealth = GetComponent<PlayerHealth>();
        playerStatus = GetComponent<PlayerStatus>();
        controler = GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        controler.Rooted = true;
        walls = GameObject.FindGameObjectsWithTag("Wall");
        List<GameObject> wallList = new List<GameObject>(walls);
        wall = wallList.Find(ob => ob.name.Equals("targetWall"));
        agent.SetDestination(wall.transform.position);
        currentHealth = playerHealth.CurrentHealth;
	}
	
	// Update is called once per frame
	void Update () {
        controler.Rooted = true;
        if (playerStatus.HasEffect(Effect.ROOTED)) {
            agent.isStopped = true;
        }
        else
            if (!playerStatus.HasEffect(Effect.ROOTED)) {
            agent.isStopped = false;
            agent.SetDestination(wall.transform.position);
        }
        if (playerHealth.CurrentHealth < 60) {
            agent.speed = 4;
        }
    }
    private void LateUpdate() {
        if (currentHealth > playerHealth.CurrentHealth && currentHealth > 80) {
            agent.SetDestination(walls[0].transform.position);
            currentHealth = playerHealth.CurrentHealth;
        }
        else
            agent.SetDestination(wall.transform.position);
    }
}
