namespace FirSkillSystem
{
    public abstract class SkillBooster
    {
        public virtual SkillCost BoosterImpact(SkillCost skillCost)
        {
            return skillCost;
        }

        public virtual void Use(ISkillUser skillUser)
        {
            
        }
    }
}
