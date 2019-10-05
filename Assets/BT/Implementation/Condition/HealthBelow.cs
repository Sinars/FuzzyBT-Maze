using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BT.Condition {
    public class HealthBelow : ConditionNode {

        private PlayerHealth health;
        private float treshold;

        public HealthBelow(Node node, GameObject enemy, float treshold) : base(node) {
            health = enemy.GetComponent<PlayerHealth>();
            this.treshold = treshold;
        }

        public override bool CheckCondition() {
            return health.CurrentHealth <= treshold;
        }
    }
}
