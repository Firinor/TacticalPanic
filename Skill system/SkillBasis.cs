using System.Collections.Generic;
using UnityEngine;

namespace FirSkillSystem
{
    public class SkillBasis
    {
        public string Name = "Skill";
        public readonly Sprite sprite;

        private float cooldown;
        private Dictionary<Gist, float> cost;

        private SkillTarget skillTarget;
        private List<SkillEffect> skillEffects;

    }
}
