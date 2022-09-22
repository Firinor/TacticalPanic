using System.Collections.Generic;
using UnityEngine;

namespace FirSkillSystem
{
    public class SkillBasis
    {
        private enum SkillTarget { Self, Target, Point }

        public string Name = "Skill";
        public string Description;
        public readonly Sprite Icon;

        public float GetCooldown()
        {
            return 0;
        }
        public Dictionary<string, int> GetCost()
        {
            return null;
        }
        public float GetDistance()
        {
            return 0;
        }

        private List<SkillNode> skillNodes;

    }
}
