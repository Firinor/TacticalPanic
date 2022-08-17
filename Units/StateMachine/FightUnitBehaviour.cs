using static UnityEngine.GraphicsBuffer;

namespace TacticalPanicCode.UnitBehaviours
{
    public class FightUnitBehaviour : UnitBehaviour
    {
        private UnitStats stats;
        private UnitOperator unit;
        private Skill skill;


        public FightUnitBehaviour(UnitOperator unit)
        {
            this.unit = unit;
            stats = unit.Stats;
            skill = unit.unitAutoAttack;
        }

        public override void FixedUpdate()
        {
            if (NoUnitToAttack())
                Exit();
        }

        private bool NoUnitToAttack()
        {
            return unit.IsThereAnyoneToAttack();
        }
    }
}
