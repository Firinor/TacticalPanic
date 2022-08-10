using UnityEngine;
using UnityEngine.UI;
using TacticalPanicCode.UnitBehaviours;
using System.Collections.Generic;
using System;
using Unity.Mathematics;

namespace TacticalPanicCode
{
    public enum VisualOfUnit { Normal, Haziness, Grayness, Off };

    //            UnitInformator
    //UnitBasis <
    //            UnitOperator -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public class UnitOperator : MonoBehaviour, IInfoble
    {
        public UnitBasis unitBasis { private get; set; }
        public UnitStats unitStats { private get; set; }
        
        [Header("Main")]
        [SerializeField]
        private Slider[] sliders = new Slider[PlayerOperator.GistsCount];
        public GistOfUnit[] GistsOfUnit { get; }
        public GistBasis[] GistBasis => unitBasis.GistBasis;
        private bool IsAlive;

        private List<UnitOperator> targets = new List<UnitOperator>();
        private List<UnitOperator> blockers = new List<UnitOperator>();
        private UnitOperator priorityTarget;

        [SerializeField]
        private SpriteRenderer pedestal;
        [SerializeField]
        private SpriteRenderer unitBodyRenderer;
        [SerializeField]
        private Rigidbody _rigidbody;
        public new Rigidbody rigidbody { get { return _rigidbody; } }
        [SerializeField]
        private Transform _skinRoot;
        public Transform SkinRoot { get => _skinRoot; }
        public float Speed { get => unitBasis.mspeed; }
        public bool Blocked { get => blockers.Count > 0; }
        public float CurrentHP { get => unitStats.CurrentHP; }
        public Sprite SpriteInfo { get => unitBasis.unitInformator.unitSprite; }

        #region Minions
        [Header("Minions")]
        [SerializeField]
        private UnitBehaviourStack unitBehaviour;
        [SerializeField]
        private AttackRadiusOperator attackRadiusOperator;
        public AttackRadiusOperator AttackRadiusOperator
        {
            get
            {
                if (attackRadiusOperator == null)
                {
                    attackRadiusOperator = gameObject.GetComponentInChildren<AttackRadiusOperator>();
                }
                return attackRadiusOperator;
            }
        }
        [SerializeField]
        private AgroRadiusOperator agroRadiusOperator;
        public AgroRadiusOperator AgroRadiusOperator
        {
            get
            {
                if (agroRadiusOperator == null)
                {
                    agroRadiusOperator = gameObject.GetComponentInChildren<AgroRadiusOperator>();
                }
                return agroRadiusOperator;
            }
        }
        [SerializeField]
        private UnitSoundOperator unitSoundOperator;
        public UnitSoundOperator UnitSoundOperator
        {
            get
            {
                if (unitSoundOperator == null)
                {
                    unitSoundOperator = gameObject.GetComponent<UnitSoundOperator>();
                }
                return unitSoundOperator;
            }
        }
        [SerializeField]
        private AnimationOperator animationOperator;
        public AnimationOperator AnimationOperator
        {
            get
            {
                if (animationOperator == null)
                {
                    animationOperator = gameObject.GetComponent<AnimationOperator>();
                }
                return animationOperator;
            }
        }
        [SerializeField]
        private UnitVFXOperator unitVFXOperator;
        public UnitVFXOperator UnitVFXOperator
        {
            get
            {
                if (unitVFXOperator == null)
                {
                    unitVFXOperator = gameObject.GetComponent<UnitVFXOperator>();
                }
                return unitVFXOperator;
            }
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            unitBasis = LoadingBattleSceneManager.LastLoadingUnit;
            gameObject.name = "Unit-" + unitBasis.unitName;
            unitBodyRenderer.sprite = SpriteInfo;
        }
        #endregion

        #region Amination
        private void Death()
        {
            unitSoundOperator.PlaySound(UnitSounds.Death);
            SetUnitActivity(false);
            AnimDeath();
        }
        private void AnimDeath()
        {
            animationOperator.AnimDeath();
        }
        public void DeathAnimationEnds()
        {
            Destroy(gameObject);
        }
        private void RefreshBar()
        {
            for (int i = 0; i < PlayerOperator.GistsCount; i++)
            {
                if (GistsOfUnit[i].slider != null)
                    GistsOfUnit[i].slider.value = GistsOfUnit[i].points;
            }
        }
        #endregion

        #region Deploy

        public void Deploy(Vector3 spawnPoint)
        {
            gameObject.transform.position = spawnPoint;
            Deploy();
        }
        public void Deploy()
        {
            animationOperator.Deploy();
            SetVisualState(VisualOfUnit.Normal);
        }

        public bool CheckTermsAndDeploy()
        {
            bool success = false;

            if (CheckTerms())
            {
                PlayerOperator.DrawMana(GetManaPrice());
                success = true;
                Deploy();
            }

            return success;
        }

        public void SpawnToPoint(UnitOnLevelPathInformator enemyPath)
        {
            Deploy(enemyPath.GetSpawnPoint());
            unitBehaviour.CreatePathBehaviour(enemyPath);
        }

        public bool CheckTerms()
        {
            bool Check = true;

            for (int i = 0; i < GistBasis.Length; i++)
            {
                int gistManaPrise = GistBasis[i].manaPrice;
                if (gistManaPrise > 0 && PlayerOperator.GetCurrentMana(GistBasis[i].gist) < gistManaPrise)
                {
                    Check = false;
                    break;
                }
            }

            return Check;
        }

        #region VisualOfDeploy
        public void Prepare(ConflictSide side)
        {
            SetConflictSide(side);
            SetVisualState(VisualOfUnit.Off);
        }
        public void SetUnitActivity(bool flag)
        {
            if (gameObject.TryGetComponent(out PlayerDebuger scriptPlayer))
                scriptPlayer.enabled = flag;
            if (gameObject.TryGetComponent(out MoveOperator scriptMoveEnemy))
                scriptMoveEnemy.enabled = flag;
            if (gameObject.TryGetComponent(out FightOperator scriptFight))
                scriptFight.enabled = flag;

            foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
            {
                collider2D.enabled = flag;
            }
        }
        public void SetConflictSide(ConflictSide side = ConflictSide.Enemy)
        {

            switch (side)
            {
                case ConflictSide.Player:
                    gameObject.tag = "Player";
                    pedestal.color = SideColor.player;
                    break;
                case ConflictSide.Neutral:
                    gameObject.tag = "Untagget";
                    pedestal.color = SideColor.neutral;
                    break;
                default: //ConflictSide.Enemy
                    gameObject.tag = "Enemy";
                    pedestal.color = SideColor.enemy;
                    break;
            }
        }
        public void SetVisualState(VisualOfUnit visual = VisualOfUnit.Normal)
        {
            switch (visual)
            {
                case VisualOfUnit.Haziness:
                    unitBodyRenderer.color = new Color(.25f, 1f, .25f, .8f);
                    break;
                case VisualOfUnit.Grayness:
                    unitBodyRenderer.color = new Color(1f, .25f, .25f, .8f);
                    break;
                case VisualOfUnit.Off:
                    unitBodyRenderer.color = new Color(1f, 1f, 1f, 0f);
                    break;
                default: //Visual.Normal
                    unitBodyRenderer.color = Color.white;
                    break;
            }
        }
        #endregion

        #region DeployManaPrice
        public string GetElementColorString(int index)
        {
            return GistColorInformator.ColorByIndex(index).TextColor;
        }
        public string GetElementColorString(Gist gist)
        {
            return GetElementColorString(PlayerOperator.GetIndexByGist(gist));
        }
        public int GetElementManaPrice(int index)
        {
            return GistBasis[index].manaPrice;
        }
        public int GetElementManaPrice(Gist gist)
        {
            return GetElementManaPrice(PlayerOperator.GetIndexByGist(gist));
        }
        public int[] GetManaPrice()
        {
            int[] price = new int[GistBasis.Length];
            for (int i = 0; i < GistBasis.Length; i++)
                price[i] = GistBasis[i].manaPrice;
            return price;
        }
        #endregion

        #endregion

        #region Damage
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
                Death();
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
        
        #region IInfoble
        public string GetName()
        {
            return unitBasis.GetTextInfo();
        }
        public void Pick()
        {
            unitBodyRenderer.material = InputOperator.PickMaterial;
        }
        public void UnPick()
        {
            unitBodyRenderer.material = InputOperator.DefaultMaterial;
        }
        public string GetTextInfo()
        {
            return GetName();
        }
        #endregion

        #region Behaviour
        public void OnAgroRadiusEnter(Collider other)
        {
            unitBehaviour.Attack();
        }
        public void OnAgroRadiusExit(Collider other)
        {
            //Calm down
        }
        public void OnAttackRadiusEnter(Collider other)
        {
            targets.Add(other.GetComponent<UnitOperator>());
        }
        public void OnAttackRadiusExit(Collider other)
        {
            targets.Remove(other.GetComponent<UnitOperator>());
        }

        internal UnitOperator EnemyToAttack()
        {
            UnitOperator resultUnit;

            if (priorityTarget != null && blockers.Contains(priorityTarget))
            {
                return priorityTarget;
            }

            if(blockers.Count > 0)
            {
                if (blockers.Count == 1)
                    return blockers[0];

                resultUnit = blockers[0];

                for(int i = 1; i < blockers.Count; i++)
                {
                    if (blockers[i].CurrentHP < resultUnit.CurrentHP)
                        resultUnit = blockers[i];
                }
            }

            //targets.Sort;
            //priorityTarget;

            return targets[0];
        }

        internal void OnBodyRadiusEnter(Collider other)
        {
            blockers.Add(other.GetComponent<UnitOperator>());
        }

        internal void OnBodyRadiusExit(Collider other)
        {
            blockers.Remove(other.GetComponent<UnitOperator>());
        }
        #endregion
    }
}