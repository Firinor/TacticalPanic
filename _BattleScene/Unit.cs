using System;
using UnityEngine;
using UnityEngine.UI;

public enum MaterialSoundType { Flesh, Wood, Metal, Stone, Spirit };
public enum UnitSounds { Death, Hit, Attack }
public enum VisualOfUnit { Normal, Haziness, Grayness, Off };

public partial class Unit : MonoBehaviour, IInfo
{
    [SerializeField]
    private string unitName = "!NO NAME!";
    public string Name { get => unitName; }

    [SerializeField]
    private Slider[] sliders = new Slider[S.GistsCount];

    private BodyElement HP;
    private BodyElement MP;
    private BodyElement EP;
    private BodyElement SP;
    private BodyElement[] elements;
    public BodyElement[] Elements { get => elements; }

    private BodyElement DeathElement;
    private bool IsAlive = true;

    [SerializeField]
    private SpriteRenderer unitSpriteRenderer;
    [SerializeField]
    private Animator unitAnimator;
    [SerializeField]
    private Sprite sprite;
    public Sprite SpriteInfo { get => sprite; }

    [SerializeField]
    private Sounds sounds;
    private AudioSourceOperator audioOperator;

    private void Awake()
    {
        unitSpriteRenderer.sprite = SpriteInfo;

        HP = new BodyElement("Health points", "red", Gist.Life, 100, 100, 1, sliders[0], 1, 10);
        MP = new BodyElement("Magic points", "#0088ff", Gist.Magic, 50, 50, 1, sliders[1], 1, 10);
        EP = new BodyElement("Energy points", "yellow", Gist.Energy, 50, 50, 1, sliders[2], 1, 5);
        SP = new BodyElement("Special points", "lime", Gist.Spectrum, 25, 25, 0, sliders[3], 1, 0);
        elements = new BodyElement[]{ HP, MP, EP, SP };

        DeathElement = HP;

        RefreshBar();

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
public class BodyElement
{
    public string name;
    public Gist point;
    public string colorString;

    public PointsValue Value;
    public int Max { get { return Value.max; } set { Value.max = value; } }
    public float Current { get { return Value.current; } set { Value.current = Math.Min(value, Value.max); } }
    public float regen;
    public Slider slider;
    public float currentAttackSpeed;
    //private float currentSkillSpeed;
    //private float currentActiveDefense;
    //private float currentPasiveDefense;
    //private float currentDodge;
    //private float currentParry;

    public int manaPrice;

    public BodyElement(string name = "", string colorString = "", Gist point = Gist.Life,
        int maxValue = 0, float currentValue = 0, float regen = 0, Slider slider = null,
        float currentAttackSpeed = 0,
        int manaPrice = 0)
    {
        this.name = name;
        this.colorString = colorString;
        this.point = point;
        this.Max = maxValue;
        this.Current = currentValue;
        this.regen = regen;
        this.slider = slider;
        this.currentAttackSpeed = currentAttackSpeed;
        this.manaPrice = manaPrice;
    }

    public struct PointsValue
    {
        public int max;
        public float current;
    }
}
