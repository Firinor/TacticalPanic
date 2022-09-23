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
        private ISkillUser unit;

        public AnimationClip anim { get; private set; }
        public VisualEffect VFXanim { get; private set; }

        private float[] cost;
        private float damage;
        private int range;
        private int targetCount;
        private List<ISkillTarget> targets;
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

        public Skill(ISkillUser unit, SkillBasis skillBasis)
        {
            this.unit = unit;
            unit.CooldownEvent += DecreaseCooldown;
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

            foreach(ISkillTarget target in targets)
            {
                target.Damage(damage);
            }

            IncreaseCooldown(cooldown);
        }

        private void IncreaseCooldown(float value)
        {
            currentCooldown += value;
            unit.CooldownEvent += DecreaseCooldown;
            Ready = false;
        }

        private void DecreaseCooldown(float deltaTime)
        {
            currentCooldown -= deltaTime;
            if(currentCooldown <= 0)
            {
                unit.CooldownEvent -= DecreaseCooldown;
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
                        unit.PaySkillRequirements(this);
                }
            }
        }

        private bool CheckTheCost()
        {
            if (cost == null)
                return true;

            //for (int i = 0; i <= PlayerOperator.GistsCount; i++)
            //{
            //    if (cost[i] > 0 &&!unit.CheckPoints(cost[i], (Gist)i))
            //    return false;
            //}

            return true;
        }

        private List<ISkillTarget> GetTargets()
        {
            return filter.TargetForSkill(unit);
        }
    }
}
