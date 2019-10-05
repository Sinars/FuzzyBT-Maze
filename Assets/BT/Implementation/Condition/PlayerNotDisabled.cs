using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BT.Condition {
    class PlayerNotDisabled : ConditionNode {

        private PlayerStatus playerStatus;
        public PlayerNotDisabled(Node action, GameObject enemy) : base(action) {
            playerStatus = enemy.GetComponent<PlayerStatus>();
        } 
        public override bool CheckCondition() {
            return !playerStatus.HasEffect(Effect.ROOTED);
        }
    }
}
