namespace TacticalPanicCode
{
    public abstract class SkillTargetFilter
    {
        public virtual UnitOperator EnemyToAttack(UnitOperator unit)
        {
            return null;
        }
    }
}
