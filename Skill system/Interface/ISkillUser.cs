using System.Collections.Generic;
using TacticalPanicCode;

namespace FirSkillSystem
{
    public delegate void CooldownEventHandler(float cooldown);

    public interface ISkillUser
    {
        public event CooldownEventHandler CooldownEvent;

        public List<SkillBasis> Skills { get; set; }

        public void UseSkill(Skill skill);

        public SkillRequirements CheckSkillRequirements(Skill skill);
    }
}
