using UnityEngine;

namespace TacticalPanicCode
{
    public partial class AnimatorEvents : MonoBehaviour
    {
        [SerializeField]
        private UnitOperator unit;

        public void Death()
        {
            unit.DeathAnimationEnds();
        }

        public void SkillUse()
        {
            unit.SkillUseAnimationPosition();
        }
    }
}
