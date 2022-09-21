namespace FirSkillSystem
{
    public abstract class SkillTargetFilter
    {
        public virtual ISkillTarget TargetForSkill(ISkillUser unit)
        {
            return null;
        }
    }
}
