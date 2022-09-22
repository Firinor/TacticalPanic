using FirSkillSystem;
using System.Collections.Generic;

namespace TacticalPanicCode
{
    public class BasicTargetFilter : SkillTargetFilter
    {
        public override List<ISkillTarget> TargetForSkill(ISkillUser owner)
        {
            UnitOperator resultUnit = null;
            List<ISkillTarget> result = new List<ISkillTarget>();

            if (owner is UnitOperator)
            {
                UnitOperator unit = (UnitOperator)owner;
                UnitOperator priorityTarget = unit.PriorityTarget;
                List<UnitOperator> blockers = unit.Blockers;
                List<UnitOperator> targets = unit.Targets;

                if (priorityTarget != null && blockers.Contains(priorityTarget))
                {
                    result.Add(priorityTarget);
                    return result;
                }

                if (blockers.Count > 0)
                {
                    if (blockers.Count == 1)
                    {
                        result.Add(blockers[0]);
                        return result;
                    }

                    resultUnit = blockers[0];

                    for (int i = 1; i < blockers.Count; i++)
                    {
                        if (blockers[i].Stats.CurrentHP < resultUnit.Stats.CurrentHP)
                            resultUnit = blockers[i];
                    }

                    result.Add(resultUnit);
                    return result;
                }

                if (priorityTarget != null)
                {
                    result.Add(priorityTarget);
                    return result;
                }

                if (targets.Count > 0)
                {
                    if (targets.Count == 1)
                    {
                        result.Add(targets[0]);
                        return result;
                    }

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

                    result.Add(resultUnit);
                    return result;
                }
            }

            result.Add(resultUnit);
            return result;
        }
    }
}
