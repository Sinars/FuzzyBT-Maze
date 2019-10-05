using System;
using UnityEngine;

namespace BT.Condition {
    class NoCooldown : ConditionNode {
        private float elapsedTime;
        private float cooldown;
        private bool attackedOnce = false;
        public NoCooldown(Node node, float cooldownTime) : base(node) {
            cooldown = cooldownTime;
            elapsedTime = Time.time;
        }

        public override bool CheckCondition() {
            if (!attackedOnce) {
                attackedOnce = true;
                return true;
            }
            if (Time.time > elapsedTime + cooldown) {
                elapsedTime = Time.time;
                return true;
            }
            return false;
        }
    }
}
