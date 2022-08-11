using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode.UnitBehaviours
{
    public class FightUnitBehaviour : UnitBehaviour
    {
        private UnitStats stats;
        private UnitOperator unit;


        public FightUnitBehaviour(UnitOperator unit)
        {
            this.unit = unit;
            stats = unit.unitStats;
        }

        internal UnitOperator EnemyToAttack()
        {
            UnitOperator resultUnit = null;

            var priorityTarget = unit.PriorityTarget;
            var blockers = unit.Blockers;
            var targets = unit.Targets;

            if (priorityTarget != null && blockers.Contains(priorityTarget))
            {
                return priorityTarget;
            }

            if (blockers.Count > 0)
            {
                if (blockers.Count == 1)
                    return blockers[0];

                resultUnit = blockers[0];

                for (int i = 1; i < blockers.Count; i++)
                {
                    if (blockers[i].unitStats.CurrentHP < resultUnit.unitStats.CurrentHP)
                        resultUnit = blockers[i];
                }
            }

            //targets.Sort;
            //priorityTarget;

            return resultUnit;
        }
    }
}
