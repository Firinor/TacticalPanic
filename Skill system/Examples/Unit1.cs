using FirSkillSystem;
using System.Collections.Generic;

internal class Unit1
{
    public void CreateSkill()
    {
        List<SkillNode> nodes = new List<SkillNode>()
        {
            new Self.Damage(Gist.Life, 12),
        };

        SkillBasis skill = new SkillBasis(nodes);
    }
}

