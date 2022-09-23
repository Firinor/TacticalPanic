using UnityEngine;
using UnityEngine.UI;
using TacticalPanicCode.UnitBehaviours;
using System.Collections.Generic;
using FirSkillSystem;

namespace TacticalPanicCode
{
    public enum VisualOfUnit { Normal, Haziness, Grayness, Off };

    //            UnitInformator
    //UnitBasis <
    //            UnitOperator,UnitStats,UnitSkills  -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public class UnitOperator : MonoBehaviour, IInfoble, ISkillUser, ISkillTarget
    {
        private UnitBasis unitBasis;
        public Skill unitAutoAttack { get; private set; }
        private Skill unitUltimate;

        [Header("Main")]
        [SerializeField]
        private Slider[] sliders = new Slider[PlayerOperator.GistsCount];

        public GistBasis[] GistBasis => unitBasis.GistBasis;
        
        public UnitDistanceToGoalHolder distanceToGoal;

        public List<UnitOperator> UnitsInAgroRadius { get; private set; } = new List<UnitOperator>();
        public List<UnitOperator> Targets { get; private set; } = new List<UnitOperator>();
        public List<UnitOperator> Blockers { get; private set; } = new List<UnitOperator>();
        public UnitOperator PriorityTarget { get; private set; }

        public bool Blocked { get => Blockers.Count > 0; }
        private CooldownEventHandler cooldownEvent;
        public event CooldownEventHandler CooldownEvent
        {
            add
            {
                cooldownEvent += value;
            }
            remove
            {
                cooldownEvent -= value;
            }
        }

        [SerializeField]
        private SpriteRenderer pedestal;
        [SerializeField]
        private SpriteRenderer unitBodyRenderer;
        [SerializeField]
        private Collider bodyCollider;
        [SerializeField]
        private Rigidbody _rigidbody;
        public Rigidbody unitRigidbody { get { return _rigidbody; } }
        [SerializeField]
        private Transform _skinRoot;
        public Transform SkinRoot { get => _skinRoot; }
        public Sprite SpriteInfo { get => unitBasis.unitInformator.unitSprite; }

        #region Minions
        [Header("Minions")]
        [SerializeField]
        private UnitBehaviourStack unitBehaviourStack;
        [SerializeField]
        private UnitStats unitStats;
        public UnitStats Stats
        {
            get
            {
                if (unitStats == null)
                {
                    unitStats = gameObject.GetComponentInChildren<UnitStats>();
                }
                return unitStats;
            }
        }
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

        public List<SkillBasis> Skills { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        internal UnitBasis GetBasis()
        {
            return unitBasis;
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            unitBasis = LoadingBattleSceneManager.LastLoadingUnit;
            gameObject.name = "Unit-" + unitBasis.unitName;
            unitBodyRenderer.sprite = SpriteInfo;
            unitStats.Death += Death;
            //unitAutoAttack = new Skill(this, SkillManager.DefaultSkill);
        }
        private void FixedUpdate()
        {
            //skills cooldown
            cooldownEvent?.Invoke(Time.fixedDeltaTime);
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
                if (unitStats.GistsOfUnit[i].slider != null)
                    unitStats.GistsOfUnit[i].slider.value = unitStats.GistsOfUnit[i].points;
            }
        }

        public void UseSkill(Skill skill)
        {
            unitVFXOperator.PlayOnce(skill.VFXanim);
            animationOperator.AnimStroke(skill, skill.anim);
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
            SetUnitActivity(true);
            attackRadiusOperator.SetVisualRangeActivity(false);
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
            unitBehaviourStack.CreatePathBehaviour(enemyPath);
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

        public bool CheckPoints(float[] cost)
        {
            bool Check = true;

            for (int i = 0; i < cost.Length; i++)
            {
                if (!CheckPoints(cost[i], (Gist)i))
                {
                    Check = false;
                    break;
                }
            }

            return Check;
        }

        public bool CheckPoints(float cost, Gist gist = Gist.Magic)
        {
            return cost > 0 && unitBasis.GistBasis[(int)gist].points < cost;
        }

        #region VisualOfDeploy
        public void Prepare(ConflictSide side)
        {
            SetConflictSide(side);
            SetVisualState(VisualOfUnit.Off);
        }
        public void SetUnitActivity(bool flag)
        {
            unitBehaviourStack.enabled = flag;
            bodyCollider.enabled = flag;
            agroRadiusOperator.SetColliderActivity(flag);
            attackRadiusOperator.SetColliderActivity(flag);
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
                    distanceToGoal = new UnitDistanceToGoalHolder();
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
            unitStats.Damage(damage);
        }
        public void Damage(float damage, Gist gist = Gist.Life)
        {
            unitStats.Damage(damage, gist);
        }
        public void Heal(float[] cure)
        {
            unitStats.Heal(cure);
        }
        public void Heal(float cure, Gist gist = Gist.Life)
        {
            unitStats.Heal(cure, gist);
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
            UnitOperator unit = other.GetComponent<UnitOperator>();
            UnitsInAgroRadius.Add(unit);
            unitBehaviourStack.Attack(unit);
        }
        public void OnAgroRadiusExit(Collider other)
        {
            UnitsInAgroRadius.Remove(other.GetComponent<UnitOperator>());
        }
        public void OnAttackRadiusEnter(Collider other)
        {
            Targets.Add(other.GetComponent<UnitOperator>());
        }
        public void OnAttackRadiusExit(Collider other)
        {
            Targets.Remove(other.GetComponent<UnitOperator>());
        }

        internal void OnBodyRadiusEnter(Collider other)
        {
            Blockers.Add(other.GetComponent<UnitOperator>());
        }

        internal void OnBodyRadiusExit(Collider other)
        {
            Blockers.Remove(other.GetComponent<UnitOperator>());
        }

        internal bool IsThereAnyoneToAttack()
        {
            return Targets.Count > 0 || Blocked || PriorityTarget != null;
        }

        internal bool NeedToGetCloserToTheTarget(UnitOperator target)
        {
            return UnitsInAgroRadius.Contains(target) && !Targets.Contains(target);
        }
        #endregion

        #region SkillInterfase
        internal void GoToTarget(UnitOperator target)
        {
            unitBehaviourStack.GoToTarget(target);
        }

        internal void SkillUseAnimationPoint(Skill skill)
        {
            
            skill.Use();
        }

        public SkillRequirements CheckSkillRequirements(Skill skill)
        {
            throw new System.NotImplementedException();
        }

        public void PaySkillRequirements(Skill skill)
        {
            throw new System.NotImplementedException();
        }
        public void PaySkillRequirements(Gist gist, int damage)
        {
            unitStats.Damage(damage, gist);
        }

        public void Buff(Buff buff)
        {
            throw new System.NotImplementedException();
        }

        public void ImpulseForse(Vector2 point, Vector3 direction, float force)
        {
            throw new System.NotImplementedException();
        }

        public void Use(string command)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}