using System;
using UniRx.Triggers;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace TacticalPanicCode
{
    //            UnitInformator
    //UnitBasis <
    //            UnitOperator,UnitStats,UnitSkills  -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public class UnitStats : MonoBehaviour, IDisposable
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        [SerializeField]
        private UnitOperator unitOperator;
        private UnitBasis unitBasis;
        public Action Death;

        public GistOfUnit[] GistsOfUnit { get; }
        public GistBasis[] GistBasis => Basis.GistBasis;

        public bool IsAlive { get { return CurrentHP <= 0; } }

        public float Speed { get => Basis.movementSpeed; }
        public float CurrentHP;

        public float currentCooldown = 0f;

        public UnitBasis Basis
        {
            get
            {
                if (unitBasis == null)
                {
                    unitBasis = unitOperator.GetBasis();
                }
                return unitBasis;
            }
        }

        void Awake()
        {
            IObservable<Unit> streamToForse = this.FixedUpdateAsObservable().Where(_ => currentCooldown > 0);
            streamToForse.Subscribe(_ => Cooldown()).AddTo(disposables);
        }

        void Cooldown()
        {
            currentCooldown -= Time.deltaTime;//Attack speed
        }

        #region Damage & Heal
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

            if (element.points <= 0 && Basis.GistOfDeath == element.gist.gist)
            {
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
        #endregion
        
        public void Dispose()
        {
            disposables.Clear();
        }
    }
}
