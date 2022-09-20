using FirSkillSystem;
using System.Collections.Generic;
using System.Numerics;

namespace TacticalPanicCode
{
    public struct SkillRequirements
    {
        public Vector2 Position { get; set; }
        public Quaternion Direction { get; set; }

        public Dictionary<Gist, float> Gists { get; set; }
        public Dictionary<Gist, int> GistBasis { get; set; }

        public float Weight { get; set; }

        public ISkillTarget target { get; set; }
        public int SummonCount { get; set; }
        public ISkillUser MySummoner { get; set; }
        public float LifeTime { get; set; }

    }
}
