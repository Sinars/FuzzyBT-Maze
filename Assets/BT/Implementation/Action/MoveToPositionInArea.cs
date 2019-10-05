using UnityEngine;
using UnityEngine.AI;

namespace BT.Action {
    public class MoveToPositionInArea : ActionNode {

        private NavMeshAgent navAgent;

        private GameObject area;

        private Vector3 target;

        public MoveToPositionInArea(GameObject character, GameObject area) {
            navAgent = character.gameObject.GetComponent<NavMeshAgent>();
            this.area = area;
            if (navAgent == null) {
                Debug.Log("GameObject doesn't have a nav agent, adding one by default");
                character.gameObject.AddComponent<NavMeshAgent>();
            }
            target = GetRandomPos();
            navAgent.SetDestination(target);
        }

        private Vector3 GetRandomPos() {
            BoxCollider boxCollider = area.GetComponent<BoxCollider>();
            return new Vector3(Random.Range(area.transform.position.x - area.transform.localScale.x * boxCollider.size.x * 0.5f,
                                                                    area.transform.position.x + area.transform.localScale.x * boxCollider.size.x * 0.5f),
                                            area.transform.position.y,
                                            Random.Range(area.transform.position.z - area.transform.localScale.z * boxCollider.size.z * 0.5f,
                                                                    area.transform.position.z + area.transform.localScale.z * boxCollider.size.z * 0.5f));

        }

        public override NodeStates Action() {
            navAgent.isStopped = false;
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance) {
                target = GetRandomPos();
                navAgent.SetDestination(target);
                return NodeStates.SUCCESS;
            }
            return NodeStates.RUNNING;
        }
    }
}