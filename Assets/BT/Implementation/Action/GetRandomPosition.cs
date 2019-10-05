using BT;
using UnityEngine;

namespace BT.Action {

    public class GetRandomPosition : ActionNode {

        private GameObject area;

        private Vector3 position;

        public GetRandomPosition(GameObject area, ref Vector3 target) {
            this.area = area;
            position = target;
        }

        public override NodeStates Action() {
            BoxCollider boxCollider = area.GetComponent<BoxCollider>();
            if (boxCollider != null) {
                position.Set(Random.Range(area.transform.position.x - area.transform.localScale.x * boxCollider.size.x * 0.5f,
                                                                      area.transform.position.x + area.transform.localScale.x * boxCollider.size.x * 0.5f),
                                             area.transform.position.y,
                                             Random.Range(area.transform.position.z - area.transform.localScale.z * boxCollider.size.z * 0.5f,
                                                                      area.transform.position.z + area.transform.localScale.z * boxCollider.size.z * 0.5f));
                return NodeStates.SUCCESS;
            }
            return NodeStates.FAILURE;
        }
    }
}