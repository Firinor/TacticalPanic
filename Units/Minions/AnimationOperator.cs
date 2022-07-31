using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class AnimationOperator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public void AnimDeath()
        {
            animator.Play("Death");
        }

        internal void Deploy()
        {
            animator.enabled = true;
        }
    }
}
