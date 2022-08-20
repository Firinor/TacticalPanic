using System.Collections.Generic;

namespace TacticalPanicCode
{
    public class BasicTargetFilter : SkillTargetFilter
    {
        public override UnitOperator TargetForSkill(UnitOperator unit)
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
