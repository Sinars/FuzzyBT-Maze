using UnityEngine;
namespace BT.Condition {

    public class PlayerNotAround : ConditionNode {

        private GameObject player;
        private GameObject aiHolder;
        private float maxDistance;


        public PlayerNotAround(Node node, GameObject holder, GameObject player, float distance) : base(node) {
            this.player = player; 
            aiHolder = holder;
            maxDistance = distance;

        }

        public override bool CheckCondition() {
            float distance = Vector3.Distance(aiHolder.transform.position, player.transform.position);
            return distance >= maxDistance;
        }
    }
}