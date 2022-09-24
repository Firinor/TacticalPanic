namespace FirSkillSystem
{
    public class Self : SkillNode
    {
        public class Buff : Self
        {
            public Buff(Buff buff) { }

        }

        public class Damage : Self
        {
            public Damage(Gist gist, int damage)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
