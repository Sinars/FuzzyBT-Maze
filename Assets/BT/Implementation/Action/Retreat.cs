using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace BT.Action {
    class Retreat : ActionNode {

        private GameObject enemy;
        private GameObject owner;
        private float retreatSpeed;

        public Retreat(GameObject owner, GameObject enemy, float retreatSpeed) {
            this.owner = owner;
            this.enemy = enemy;
            this.retreatSpeed = retreatSpeed;
        }


        private void Move() {
            owner.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Vector3 heading = enemy.transform.position - owner.transform.position;
            Vector3 direction = heading / heading.magnitude;
            owner.transform.LookAt(enemy.transform);
            owner.transform.Translate(direction * retreatSpeed * Time.deltaTime);
        }

        public override NodeStates Action() {
            Move();
            return NodeStates.SUCCESS;
        }
    }
}
