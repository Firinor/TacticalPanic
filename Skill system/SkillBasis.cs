using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class SkillBasis
    {
        public string Name = "Skill";
        public readonly Sprite sprite;

        private float cooldown;
        private float cost;

        private List<SkillNode> skillNodes;

        public bool CheckTerms()
        {
            foreach (SkillNode node in skillNodes)
            {
                if (!node.CheckTerms())
                {
                    return false;
                }
            }

            return true;
        }


    }
}
