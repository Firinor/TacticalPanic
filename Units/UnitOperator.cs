using UnityEngine;
using UnityEngine.UI;
using TacticalPanicCode.UnitBehaviours;
using System.Collections.Generic;
using System;

namespace TacticalPanicCode
{
    public enum MaterialSoundType { Flesh, Wood, Metal, Stone, Spirit };
    public enum UnitSounds { Death, Hit, Attack }
    public enum VisualOfUnit { Normal, Haziness, Grayness, Off };

    //            UnitInformator
    //UnitBasis <
    //            UnitOperator -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public partial class UnitOperator : MonoBehaviour, IInfoble
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

        [SerializeField]
        private SpriteRenderer pedestal;
        [SerializeField]
        private SpriteRenderer unitBodyRenderer;
        [SerializeField]
        private Rigidbody _rigidbody;
        public new Rigidbody rigidbody { get { return _rigidbody; } }
        [SerializeField]
        private Transform _skinRoot;
        public Transform skinRoot { get { return _skinRoot; } }

        public float speed { get { return unitBasis.mspeed; } }

        public Sprite SpriteInfo { get => unitBasis.unitInformator.unitSprite; }

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

        internal UnitOperator NearestEnemy()
        {
            throw new NotImplementedException();
        }

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

        internal UnitOperator MostDangerousEnemy()
        {
            throw new NotImplementedException();
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

        private void Awake()
        {
            unitBasis = LoadingBattleSceneManager.LastLoadingUnit;
            gameObject.name = "Unit-" + unitBasis.unitName;
            unitBodyRenderer.sprite = SpriteInfo;
        }
    }
}