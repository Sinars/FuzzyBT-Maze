using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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

    private RaycastHit raycastHit;
    // Use this for initialization
    void Start () {
        target = GameManagement.GM.player;
        behaviour = GetComponent<SpiderBehaviour>();
        targetDistance = behaviour.stoppingDistance;
        health = target.GetComponent<PlayerHealth>();
        status = target.GetComponent<PlayerStatus>();
	}
	
    public void Attack()
    {
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out raycastHit,
            targetDistance)) return;
        Debug.Log("Hit");
        transform.LookAt(target.transform);
        health.takeDamage(damage);
    }
    
    public void PoisonAttack()
    {
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out raycastHit,
            targetDistance)) return;
        transform.LookAt(target.transform);
        health.takeDamage(poisonDamage);
        status.AddBuff(new Buff(Effect.POISONED, poisonDamage, poisionRate));
        poisoned = true;
        elapsedTime = Time.time;
    }

    public void LateUpdate() {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*targetDistance, Color.blue);
        if (poisoned && Time.time > elapsedTime + poisonTime) {
            poisoned = false;
            status.NegateBuff(Effect.POISONED);
        }
    }
}
