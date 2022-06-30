using System;
using UnityEngine;
using UnityEngine.UI;

public enum MaterialSoundType { Flesh, Wood, Metal, Stone, Spirit };
public enum UnitSounds { Death, Hit, Attack }
public enum VisualOfUnit { Normal, Haziness, Grayness, Off };

//            UnitInformator
//UnitBasis <
//            Unit -> UnitCard
//
// Gist -> GistBasis -> GistOfUnit
public partial class Unit : MonoBehaviour, IInfoble
{
    public UnitBasis unitBasis { private get; set; }

    [SerializeField]
    private Slider[] sliders = new Slider[PlayerOperator.GistsCount];
    public GistOfUnit[] GistsOfUnit { get; }
    public GistBasis[] GistBasis => unitBasis.GistBasis;
    

    private bool IsAlive;
    [SerializeField]
    private SpriteRenderer pedestal;
    [SerializeField]
    private SpriteRenderer unitSpriteRenderer;
    [SerializeField]
    private Animator unitAnimator;
    public Sprite SpriteInfo { get => unitBasis.unitInformator.unitSprite; }

    [SerializeField]
    private Sounds sounds;
    private AudioSourceOperator audioOperator;

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

        sounds.SetSounds(SoundInformator.GetUnitSounds(sounds.GetMaterials()));
        audioOperator = GetComponentInChildren<AudioSourceOperator>();
    }

    [Serializable]
    private class Sounds
    {
        [HideInInspector]
        public AudioClip Death;
        [HideInInspector]
        public AudioClip Hit;
        [HideInInspector]
        public AudioClip Attack;

        [SerializeField]
        private MaterialSoundType DeathSoundType;
        [SerializeField]
        private MaterialSoundType HitSoundType;
        [SerializeField]
        private MaterialSoundType AttackSoundType;

        public void SetSounds(AudioClip[] clips)
        {
            if (clips != null && clips.Length == 3)
            {
                Death = clips[0];
                Hit = clips[1];
                Attack = clips[2];
            }
        }

        public MaterialSoundType[] GetMaterials()
        {
            return new MaterialSoundType[] { DeathSoundType, HitSoundType, AttackSoundType };
        }
    }
}

[Serializable]
public class GistOfUnit
{
    public Slider slider;

    public GistBasis gist;
    public int points;
    public int attack;
    public int defense;
    public float reattack;
    public float regen;
    public float moveSpeed;

    public int manaPrice;

    //private float currentSkillSpeed;
    //private float currentActiveDefense;
    //private float currentPasiveDefense;
    //private float currentDodge;
    //private float currentParry;

    public GistOfUnit(GistBasis gist, Slider slider = null)
    {
        this.slider = slider;
        this.gist = gist;
        points = gist.points;
        attack = gist.attack;
        defense = gist.defense;
        reattack = gist.reattack;
        regen = gist.regen;
        moveSpeed = gist.moveSpeed;
        manaPrice = gist.manaPrice;
    }
}