using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour {

    private GameObject target;
    private SpiderBehaviour behaviour;
    private float targetDistance;
    public float damage;
    public float poisonDamage;
    public float poisionRate;
    public float attackRate;
    public float poisonTime;
    private bool poisoned;
    private PlayerHealth health;
    private PlayerStatus status;
    private float elapsedTime;
    // Use this for initialization
    void Start () {
        target = GameManagement.GM.player;
        behaviour = GetComponent<SpiderBehaviour>();
        targetDistance = behaviour.stoppingDistance;
        health = target.GetComponent<PlayerHealth>();
        status = target.GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
    public void Attack() {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        transform.LookAt(target.transform);
        Debug.Log(distance + " " + targetDistance);
        if (distance <= targetDistance) {
            health.takeDamage(damage);
        }
    }
    
    public void PoisonAttack() {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        transform.LookAt(target.transform);
        if (distance <= targetDistance) {
            health.takeDamage(poisonDamage);
            status.AddBuff(new Buff(Effect.POISONED, poisonDamage, poisionRate));
            poisoned = true;
            elapsedTime = Time.time;
        }
    }

    public void LateUpdate() {
        if (poisoned && Time.time > elapsedTime + poisonTime) {
            poisoned = false;
            status.NegateBuff(Effect.POISONED);
        }
    }
}
