namespace TacticalPanicCode
{
    public abstract class TargetFilter
    {
        public virtual UnitOperator EnemyToAttack(UnitOperator unit)
        {
            return null;
        }
    }
}
