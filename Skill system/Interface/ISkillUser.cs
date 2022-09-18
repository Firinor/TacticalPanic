namespace FirSkillSystem
{
    public delegate void CooldownEventHandler(float cooldown);

    public interface ISkillUser
    {
        public event CooldownEventHandler CooldownEvent;

        public abstract void UseSkill(Skill skill);

        #region Damage
        public abstract void Damage(float damage, Gist gist = Gist.Life);
        public abstract void Heal(float cure, Gist gist = Gist.Life);
        #endregion

        public abstract bool CheckPoints(float cost, Gist gist = Gist.Magic);
    }
}
