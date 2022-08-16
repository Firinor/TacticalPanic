using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace TacticalPanicCode
{
    //            UnitInformator
    //UnitBasis <
    //            UnitOperator,UnitStats -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public class Skill
    {
        private UnitOperator unit;

        private float[] cost;
        private int damage;
        private int range;
        private int targetCount;
        private int cooldown;
        private int currentCooldown;
        private SkillTargetFilter filter = new BasicTargetFilter();

        //Attack states
        private int prepareWeight;
        private int effeckWeight;
        private int cooldownWeight;

        private int preparePart;
        private int effeckPart;
        private int cooldownPart;

        public void Prepare()
        {
            if (currentCooldown > 0)
                return;

            List<UnitOperator> targets = GetTargets();
        }

        public void Use()
        {
            var targets = GetTargets();

            if(targets == null)
                return;

            if (!CheckTheCost())
                return;

            PayTheCost();

            foreach(UnitOperator target in targets)
            {
                target.Damage(damage);
            }

            IncreaseCooldown();
        }

        private void IncreaseCooldown()
        {
            currentCooldown += cooldown;
        }

        private void PayTheCost()
        {
            unit.Damage(cost);
        }

        private bool CheckTheCost()
        {
            if (cost == null)
                return true;

            return unit.CheckPoints(cost);
        }

        private List<UnitOperator> GetTargets()
        {
            List<UnitOperator> targets = new List<UnitOperator>();
            UnitOperator result = filter.EnemyToAttack(unit);
            if (result != null)
                targets.Add(result);

            return targets;
        }
    }
}
