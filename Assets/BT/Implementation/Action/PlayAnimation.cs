using System;
using UnityEngine;

namespace BT.Action {


    class PlayAnimation : ActionNode {

        private Animator animator;

        private string animation;

        public PlayAnimation(GameObject owner, String animationName) : base() {
            animator = owner.GetComponent<Animator>();
            animation = animationName;
        }

        public override NodeStates Action() {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) {
                animator.Play(animation);
                return NodeStates.RUNNING;
            }
            return NodeStates.SUCCESS;

        }
    }
}