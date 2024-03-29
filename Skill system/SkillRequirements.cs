using FirSkillSystem;
using System.Collections.Generic;
using System.Numerics;

namespace TacticalPanicCode
{
    public class SkillRequirements
    {
        public bool NeedPosition { get; set; }
        public Vector2 Position { get; set; }
        public bool NeedDirection { get; set; }
        public Quaternion Direction { get; set; }

        public Dictionary<Gist, float> Gists { get; set; }
        public Dictionary<Gist, int> GistBasis { get; set; }

        public bool NeedWeight { get; set; }
        public float Weight { get; set; }

        public ISkillTarget target { get; set; }
        public int SummonCount { get; set; }
        public ISkillUser MySummoner { get; set; }
        public float LifeTime { get; set; }

    }
}
