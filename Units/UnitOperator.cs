using System;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public enum MaterialSoundType { Flesh, Wood, Metal, Stone, Spirit };
    public enum UnitSounds { Death, Hit, Attack }
    public enum VisualOfUnit { Normal, Haziness, Grayness, Off };

    //            UnitInformator
    //UnitBasis <
    //            Unit -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public partial class UnitOperator : MonoBehaviour, IInfoble
    {
        public UnitBasis unitBasis { private get; set; }

        [Header("Main")]
        [SerializeField]
        private Slider[] sliders = new Slider[PlayerOperator.GistsCount];
        public GistOfUnit[] GistsOfUnit { get; }
        public GistBasis[] GistBasis => unitBasis.GistBasis;
        private bool IsAlive;

        [SerializeField]
        private UnitBehaviorStack unitBehaviour;
        [SerializeField]
        private SpriteRenderer pedestal;
        [SerializeField]
        private SpriteRenderer unitSpriteRenderer;
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
            unitSpriteRenderer.sprite = SpriteInfo;

            //HP = new GistOfUnit("Health points", "red", Gist.Life, 100, 100, 1, sliders[0], 1, 10);
            //MP = new GistOfUnit("Magic points", "#0088ff", Gist.Magic, 50, 50, 1, sliders[1], 1, 10);
            //EP = new GistOfUnit("Energy points", "yellow", Gist.Energy, 50, 50, 1, sliders[2], 1, 5);
            //SP = new GistOfUnit("Special points", "lime", Gist.Spectrum, 25, 25, 0, sliders[3], 1, 0);
            //elements = new GistOfUnit[]{ HP, MP, EP, SP };

            //RefreshBar();
        }
    }
}