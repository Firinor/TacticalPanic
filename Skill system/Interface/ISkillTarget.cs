using UnityEngine;

namespace FirSkillSystem
{
    public interface ISkillTarget
    {
        public void Damage(float damage, Gist gist = Gist.Life);

        public void Buff(Buff buff);

        public void ImpulseForse(Vector2 point, Vector3 direction, float force);

        //public void Summon(GameObject summon);

        public void Use(string command);
    }
}
