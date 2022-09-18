namespace FirSkillSystem
{
    public abstract class SkillTargetFilter
    {
        public virtual ISkillUser TargetForSkill(ISkillUser unit)
        {
            return null;
        }
    }
}
