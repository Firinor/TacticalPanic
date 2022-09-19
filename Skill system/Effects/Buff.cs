using System.Collections.Generic;

namespace FirSkillSystem
{
    public class Buff
    {
        private ISkillTarget skillTarget;
        private SkillBasis parent;
        private List<SkillBasis> children;
        private float lifetime;

        public Buff(ISkillTarget skillTarget, SkillBasis parent)
        {
            this.skillTarget = skillTarget;
            this.parent = parent;
        }

        public void OnFinished()
        {

        }
    }
}
