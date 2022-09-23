using System.Collections.Generic;
using UnityEngine;

namespace FirSkillSystem
{
    public class SkillBasis
    {
        public int ID;
        public string Name = "Skill";
        public string Description;
        public Sprite Icon;

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

        public SkillTargetFilter Filter;
        public float Distance;
        public List<SkillBooster> skillBoosters;
        public List<SkillNode> skillNodes;

    }
}
