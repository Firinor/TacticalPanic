namespace TacticalPanicCode
{
    public abstract class SkillTargetFilter
    {
        public virtual UnitOperator TargetForSkill(UnitOperator unit)
        {
            return null;
        }
    }
}
