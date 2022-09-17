using TacticalPanicCode;
using UnityEditor;
using UnityEngine;

namespace FirSkillSystem
{
    public interface IUnit
    {
        public delegate void UnitFixedUpdate(float i);
        public UnitFixedUpdate unitFixedUpdate { get; set; }

        public abstract void UseSkill(Skill skill);

        #region Damage
        public abstract void Damage(float damage, Gist gist = Gist.Life);
        public abstract void Heal(float cure, Gist gist = Gist.Life);
        #endregion

        public bool CheckPoints(float cost, Gist gist = Gist.Magic);
    }
}
