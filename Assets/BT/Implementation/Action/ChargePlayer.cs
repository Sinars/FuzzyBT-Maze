using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace BT.Action {
    public class ChargePlayer : ActionNode {
        private NavMeshAgent navAgent;

        private GameObject target;

        private GameObject owner;

        private float attackDistance;

        private Vector3 targetPosition;

        public ChargePlayer(GameObject character, GameObject target, float distance) {
            owner = character;
            navAgent = character.gameObject.GetComponent<NavMeshAgent>();
            if (navAgent == null) {
                Debug.Log("GameObject doesn't have a nav agent, adding one by default");
                character.gameObject.AddComponent<NavMeshAgent>();
            }
            this.target = target;
            attackDistance = distance;
        }
        public override NodeStates Action() {
            navAgent.isStopped = false;
            targetPosition = target.transform.position;
            navAgent.SetDestination(targetPosition);
            if (navAgent.remainingDistance <= attackDistance) {
                navAgent.isStopped = true;
                owner.transform.LookAt(target.transform);
                //Debug.Log("Target found: " + navAgent.remainingDistance);

                return NodeStates.SUCCESS;
            }
            return NodeStates.RUNNING;
        }
    }

}