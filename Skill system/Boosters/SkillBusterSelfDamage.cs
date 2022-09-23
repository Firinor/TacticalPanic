namespace FirSkillSystem
{
    public class SkillBusterSelfDamage : SkillBooster
    {
        private int damage;
        private Gist gist;

        public SkillBusterSelfDamage(int damage, Gist gist)
        {
            this.damage = damage;
            this.gist = gist;
        }

        public override SkillCost BoosterImpact(SkillCost skillCost)
        {
            return skillCost;
        }

        public override void Use(ISkillUser skillUser)
        {
            skillUser.PaySkillRequirements(gist, damage);
        }
    }
}
