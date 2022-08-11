using System;
using Unity.Mathematics;
using UnityEngine;

namespace TacticalPanicCode
{
    public class UnitStats : MonoBehaviour
    {
        public Action Death;
        private UnitBasis unitBasis;
        [SerializeField]
        private UnitOperator unitOperator;

        public GistOfUnit[] GistsOfUnit { get; }
        public GistBasis[] GistBasis => unitBasis.GistBasis;

        public bool IsAlive;

        public float Speed { get => unitBasis.mspeed; }
        public float CurrentHP;

        public void Damage(float[] damage)
        {
            for (int i = 0; i < damage.Length && i < PlayerOperator.GistsCount; i++)
            {
                if (damage[i] != 0 && GistsOfUnit[i].slider != null)
                {
                    Damage(damage[i], GistsOfUnit[i]);
                }
            }
        }
        public void Damage(float damage, Gist gist = Gist.Life)
        {
            if (damage == 0)
                return;

            Damage(damage, GistBasis[PlayerOperator.GetIndexByGist(gist)].gist);
        }
        private void Damage(float damage, GistOfUnit element)
        {
            if (!IsAlive || damage == 0 || element.slider == null || element.points <= 0)
                return;

            element.points -= (int)damage;
            element.points = math.clamp(element.points, 0, element.gist.points);
            element.slider.value = element.points;

            if (element.points <= 0 && unitBasis.GistOfDeath == element.gist.gist)
            {
                IsAlive = false;
                Death?.Invoke();
            }
            else
            {
                //audioOperator.PlaySound(UnitSounds.Hit, this);
            }
            //UnitInfoPanelOperator.RefreshPointsInfo(gameObject.GetComponent<UnitOperator>());
        }
        public void Heal(float[] cure)
        {
            for (int i = 0; i < cure.Length && i < PlayerOperator.GistsCount; i++)
            {
                if (cure[i] != 0 && GistsOfUnit[i].slider != null)
                {
                    Damage(-cure[i], GistsOfUnit[i]);
                }
            }
        }
        public void Heal(float cure, Gist gist = Gist.Life)
        {
            if (cure == 0)
                return;

            Damage(-cure, GistBasis[PlayerOperator.GetIndexByGist(gist)].gist);
        }
    }
}
