namespace FirSkillSystem
{
    public abstract class SkillTargetFilter
    {
        public virtual IUnit TargetForSkill(IUnit unit)
        {
            return null;
        }
    }
}
