using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    //            UnitInformator
    //UnitBasis <
    //            UnitOperator,UnitStats -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public class Skill
    {
        public bool Ready { get; private set; }

        private SkillBasis skillBasis;
        private UnitOperator unit;

        private AnimationClip amin;

        private float[] cost;
        private int damage;
        private int range;
        private int targetCount;
        private float cooldown;
        private float currentCooldown;
        private SkillTargetFilter filter = new BasicTargetFilter();

        //Attack states
        private int prepareWeight;
        private int effeckWeight;
        private int cooldownWeight;

        private float preparePart;
        private float effeckPart;
        private float cooldownPart;

        public Skill(UnitOperator unit, SkillBasis skillBasis)
        {
            this.unit = unit;
            unit.unitFixedUpdate += DecreaseCooldown;
            this.skillBasis = skillBasis;
        }

        public void Prepare()
        {
            if (currentCooldown > 0)
                return;

            IncreaseCooldown(preparePart);
            unit.UseSkill(this, amin);
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

            IncreaseCooldown(cooldown);
        }

        private void IncreaseCooldown(float value)
        {
            currentCooldown += value;
            unit.unitFixedUpdate += DecreaseCooldown;
            Ready = false;
        }

        private void DecreaseCooldown(float deltaTime)
        {
            currentCooldown -= deltaTime;
            if(currentCooldown <= 0)
            {
                unit.unitFixedUpdate -= DecreaseCooldown;
                Ready = true;
            }
        }

        private void PayTheCost()
        {
            if (cost != null)
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
            UnitOperator result = filter.TargetForSkill(unit);
            if (result != null)
                targets.Add(result);

            return targets;
        }
    }
}
