using FirSkillSystem;
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
        [SerializeField]
        private AnimatorEvents animatorEvents;

        public void AnimDeath()
        {
            animator.Play("Death");
        }
        internal void Deploy()
        {
            animator.enabled = true;
        }
        internal void AnimStroke(Skill skill, AnimationClip amin)
        {
            animatorEvents.SetSkill(skill);
            string name;
            if(amin == null)
            {
                name = "Stroke";
            }
            else
            {
                name = amin.name;
            }
            animator.Play(name);
        }
    }
}
