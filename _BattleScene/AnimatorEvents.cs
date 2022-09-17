using FirSkillSystem;
using UnityEngine;

namespace TacticalPanicCode
{
    public partial class AnimatorEvents : MonoBehaviour
    {
        [SerializeField]
        private UnitOperator unit;
        private Skill skill;

        public void Death()
        {
            unit.DeathAnimationEnds();
        }

        public void SetSkill(Skill skill)
        {
            this.skill = skill;
        }

        public void SkillUse()
        {
            unit.SkillUseAnimationPoint(skill);
        }
    }
}
