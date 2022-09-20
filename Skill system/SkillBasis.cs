using System.Collections.Generic;
using UnityEngine;

namespace FirSkillSystem
{
    public class SkillBasis
    {
        private enum SkillEffect { Damage, Buff, ImpulseForse, Summon, Use }

        public string Name = "Skill";
        public readonly Sprite sprite;

        private float cooldown;
        private Dictionary<Gist, float> cost;

        private SkillZone skillTarget;
        private List<SkillEffect> skillEffects;

    }
}
