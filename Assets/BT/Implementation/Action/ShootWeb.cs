using System;
using UnityEngine;

namespace BT.Action {
    class ShootWeb : ActionNode {

        private GameObject web;
        private GameObject owner;
        private PlayerStatus target;
        private float fireRate;
        private float elapsedTime;


        public ShootWeb(GameObject owner, GameObject web, GameObject target) {

            this.web = web;
            this.owner = owner;
            this.target = target.GetComponent<PlayerStatus>();
        }

        public override NodeStates Action() {
            if (!target.HasEffect(Effect.ROOTED)) {
                owner.transform.LookAt(target.gameObject.transform);
                GameObject temp = GameObject.Instantiate(web, owner.transform.position, Quaternion.identity);
                return temp == null ? NodeStates.FAILURE : NodeStates.SUCCESS;
            }
            return NodeStates.SUCCESS;
        }
    }
}
