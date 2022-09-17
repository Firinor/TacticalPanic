using FirSkillSystem;

namespace TacticalPanicCode.UnitBehaviours
{
    public class FightUnitBehaviour : UnitBehaviour
    {
        private UnitOperator ownerUnit;
        private UnitOperator target;
        private Skill skill;

        public FightUnitBehaviour(UnitOperator ownerUnit, UnitOperator target)
        {
            this.ownerUnit = ownerUnit;
            skill = ownerUnit.unitAutoAttack;
            this.target = target;
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

            if (NeedToGetCloserToTheTarget())
            {
                ownerUnit.GoToTarget(target);
                return;
            }

            skill.Prepare();
        }

        private bool NeedToGetCloserToTheTarget()
        {
            return ownerUnit.NeedToGetCloserToTheTarget(target);
        }

        private bool NoUnitToAttack()
        {
            return !ownerUnit.IsThereAnyoneToAttack();
        }
    }
}
