using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
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
            stats = unit.Stats;
        }

        public override void FixedUpdate()
        {
            if (stats.attackAction)
            {
                if (stats.readyToAttack)
                {
                    stats.readyToAttack = false;
                    stats.currentCooldown = 0f;
                    stats.attackStage = UnitStats.AttackStages.swing;
                }

                if (stats.attackStage == UnitStats.AttackStages.swing)
                {
                    if (stats.currentCooldown > stats.TimeToSwing)
                    {
                        stats.attackStage = UnitStats.AttackStages.arc;
                        //stats.currentArcCooldown = 0;
                        //AreaDamage();
                    }
                }
                else if (stats.attackStage == UnitStats.AttackStages.arc)
                {
                    if (stats.currentCooldown > stats.TimeToArcOff)
                    {
                        stats.attackStage = UnitStats.AttackStages.rollback;
                    }
                }
                else if (stats.attackStage == UnitStats.AttackStages.rollback)
                {
                    //if (stats.currentCooldown >= stats.Cooldown)
                    //{
                    //    stats.readyToAttack = true;
                    //    stats.attackAction = false;
                    //}
                }
            }
        }

        internal UnitOperator EnemyToAttack()
        {
            UnitOperator resultUnit = null;

            UnitOperator priorityTarget = unit.PriorityTarget;
            List<UnitOperator> blockers = unit.Blockers;
            List<UnitOperator> targets = unit.Targets;

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
                    if (blockers[i].Stats.CurrentHP < resultUnit.Stats.CurrentHP)
                        resultUnit = blockers[i];
                }

                return resultUnit;
            }

            if (priorityTarget != null)
            {
                return priorityTarget;
            }

            if (targets.Count > 0)
            {
                if (targets.Count == 1)
                    return targets[0];

                resultUnit = targets[0];
                float distanceOfGoal = resultUnit.distanceToGoal.DistanceToGoal(resultUnit.transform.position);

                for (int i = 1; i < targets.Count; i++)
                {
                    float pretendentValue = targets[i].distanceToGoal.DistanceToGoal(targets[i].transform.position);
                    if (pretendentValue < distanceOfGoal)
                    {
                        resultUnit = targets[i];
                        distanceOfGoal = pretendentValue;
                    }
                }

                return resultUnit;
            }
            
            return resultUnit;
        }
    }
}
