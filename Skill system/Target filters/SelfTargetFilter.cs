using FirSkillSystem;
using System.Collections.Generic;

namespace TacticalPanicCode
{
    public class SelfTargetFilter : SkillTargetFilter
    {
        public override List<ISkillTarget> TargetForSkill(ISkillUser owner)
        {
            if (owner is ISkillTarget)
            {
                return new List<ISkillTarget> { (ISkillTarget)owner };
            }
            return null;
        }
    }
}
