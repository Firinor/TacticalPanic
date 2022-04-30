using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public partial class Stats : MonoBehaviour
{
    public enum Visual { Normal, Haziness, Grayness, Off };

    [SerializeField]
    private string unitName = "!NO NAME!";
    public string Name { get => unitName; }

    [Serializable]
    private class BodyElement
    {
        public string name;
        public Gist point;
        public string colorString;

        public int maxValue;
        public float currentValue;
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
                this.maxValue = maxValue;
                this.currentValue = currentValue;
                this.regen = regen;
                this.slider = slider;
                this.currentAttackSpeed = currentAttackSpeed;
                this.manaPrice = manaPrice;
            }
    }

    [SerializeField]
    private Slider[] sliders = new Slider[S.GistsCount];

    private BodyElement HP;
    private BodyElement MP;
    private BodyElement EP;
    private BodyElement SP;
    private BodyElement[] Element;

    private BodyElement DeathElement;
    private bool IsAlive = true;

    [SerializeField]
    private SpriteRenderer unitSpriteRenderer;
    [SerializeField]
    private Sprite unitSprite;
    public Sprite UnitSprite { get => unitSprite; }

    private void Awake()
    {
        unitSpriteRenderer.sprite = UnitSprite;

        HP = new BodyElement("Health points", "red", Gist.Life, 100, 100, 1, sliders[0], 1, 10);
        MP = new BodyElement("Magic points", "#0088ff", Gist.Magic, 50, 50, 1, sliders[1], 1, 10);
        EP = new BodyElement("Energy points", "yellow", Gist.Energy, 50, 50, 1, sliders[2], 1, 10);
        SP = new BodyElement("Special points", "lime", Gist.Spectrum, 25, 25, 0, sliders[3], 1, 0);
        Element = new BodyElement[]{ HP, MP, EP, SP };

        DeathElement = HP;

        RefreshBar();
    }
}
