using System;
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
            if (!skill.Ready)
                return;

            if (NoUnitToAttack())
            {
                Exit();
                return;
            }

            if (!TargetInRadius())
            {
                unit.GoToTarget();
                return;
            }

            skill.Prepare();
        }

        private bool TargetInRadius()
        {
            throw new NotImplementedException();
        }

        private bool NoUnitToAttack()
        {
            return !unit.IsThereAnyoneToAttack();
        }
    }
}
