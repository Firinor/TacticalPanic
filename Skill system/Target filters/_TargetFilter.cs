using System.Collections.Generic;

namespace FirSkillSystem
{
    public abstract class SkillTargetFilter
    {
        public virtual List<ISkillTarget> TargetForSkill(ISkillUser unit)
        {
            return null;
        }
    }
}
