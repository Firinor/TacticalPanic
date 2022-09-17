using System.Collections.Generic;
using TacticalPanicCode;
using UnityEngine;
using UnityEngine.VFX;

namespace FirSkillSystem
{
    public class Skill
    {
        public bool Ready { get; private set; }

        private SkillBasis skillBasis;
        private IUnit unit;

        public AnimationClip anim { get; private set; }
        public VisualEffect VFXanim { get; private set; }

        private float[] cost;
        private float damage;
        private int range;
        private int targetCount;
        private float cooldown;
        private float currentCooldown;
        private SkillTargetFilter filter;

        //Attack states
        private int prepareWeight;
        private int effeckWeight;
        private int cooldownWeight;

        private float preparePart;
        private float effeckPart;
        private float cooldownPart;

        public Skill(IUnit unit, SkillBasis skillBasis)
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
            unit.UseSkill(this);
        }

        public void Use()
        {
            var targets = GetTargets();

            if(targets == null)
                return;

            if (!CheckTheCost())
                return;

            PayTheCost();

            foreach(IUnit target in targets)
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
            {
                for (int i = 0; i <= PlayerOperator.GistsCount; i++)
                {
                    if (cost[i] > 0)
                        unit.Damage(cost[i], (Gist)i);
                }
            }
        }

        private bool CheckTheCost()
        {
            if (cost == null)
                return true;

            for (int i = 0; i <= PlayerOperator.GistsCount; i++)
            {
                if (cost[i] > 0 &&!unit.CheckPoints(cost[i], (Gist)i))
                return false;
            }

            return true;
        }

        private List<IUnit> GetTargets()
        {
            List<IUnit> targets = new List<IUnit>();
            IUnit result = filter.TargetForSkill(unit);
            if (result != null)
                targets.Add(result);

            return targets;
        }
    }
}
