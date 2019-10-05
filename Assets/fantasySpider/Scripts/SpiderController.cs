using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderController : MonoBehaviour {

    public float damage;

    private GameObject player;

    private PlayerHealth playerHealth;

    private Animator animator;

    [SerializeField]
    private string attackAnimation;

    private float attackRate = 0.5f;

    public float playerDistance;

    private float elapsedTime = 0;
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        player = GameManagement.GM.player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public void Update() {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distance < playerDistance)
        if (Time.time > elapsedTime && !animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation)) {
            playerHealth.takeDamage(damage);
            elapsedTime = Time.time + attackRate;
        }
    }
}
