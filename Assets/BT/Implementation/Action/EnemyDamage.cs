using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BT.Action {
    class EnemyDamage : ActionNode {

        private float damage;
        private float attackRate;
        private PlayerHealth health;
        private float elapsedTime;
        private GameObject owner;
        private GameObject enemy;

        public EnemyDamage(GameObject owner, GameObject enemy, float damage, float attackRate) {
            this.enemy = enemy;
            health = enemy.GetComponent<PlayerHealth>();
            elapsedTime = Time.time;
            this.owner = owner;
            this.damage = damage;
            this.attackRate = attackRate;
        }

        public override NodeStates Action() {
            owner.transform.LookAt(enemy.transform);
            if (Time.time > elapsedTime + attackRate) {
                health.takeDamage(damage);
                elapsedTime = Time.time;
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
    }
}
