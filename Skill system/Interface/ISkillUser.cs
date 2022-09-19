using System.Collections.Generic;
using System.Numerics;

namespace FirSkillSystem
{
    public delegate void CooldownEventHandler(float cooldown);

    public interface ISkillUser
    {
        public event CooldownEventHandler CooldownEvent;

        public List<SkillBasis> Skills { get; set; }

        public Vector2 Position { get; set; }
        public Quaternion Direction { get; set; }

        public Dictionary<Gist, float> Gists { get; set; }
        public Dictionary<Gist, int> GistBasis { get; set; }

        public float Weight { get; set; }

        public ISkillTarget target { get; set; }
        public int SummonCount { get; set; }
        public ISkillUser MySummoner { get; set; }
        public float LifeTime { get; set; }

        public void UseSkill(Skill skill);

        #region Damage
        public void Damage(float damage, Gist gist = Gist.Life);
        public void Heal(float cure, Gist gist = Gist.Life);
        #endregion

        public bool CheckPoints(float cost, Gist gist = Gist.Magic);
    }
}
