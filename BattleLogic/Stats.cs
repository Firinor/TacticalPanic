using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public partial class Stats : MonoBehaviour
{
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
    private Slider[] sliders = new Slider[4];

    private BodyElement HP;
    private BodyElement MP;
    private BodyElement EP;
    private BodyElement SP;
    private BodyElement[] Element;

    private BodyElement DeathElement;

    void Start()
    {
        HP = new BodyElement("Health points", "red", Gist.Life, 100, 100, 1, sliders[0], 1, 10);
        MP = new BodyElement("Magic points", "#0088ff", Gist.Magic, 50, 50, 1, sliders[1], 1, 10);
        EP = new BodyElement("Energy points", "yellow", Gist.Energy, 50, 50, 1, sliders[2], 1, 10);
        SP = new BodyElement("Special points", "lime", Gist.Spectrum, 25, 25, 0, sliders[3], 1, 10);
        Element = new BodyElement[]{ HP, MP, EP, SP };

        DeathElement = HP;

        RefreshBar();
    }

    private void Death()
    {
        DisableAllScripts();
        AnimDeath();
    }

    private void DisableAllScripts()
    {
        gameObject.GetComponent<Player>()?.Deactivate();
        gameObject.GetComponent<MoveEnemy>()?.Deactivate();
        gameObject.GetComponent<Fight>()?.Deactivate();
        
        foreach (Collider2D collider2D in gameObject.GetComponents<Collider2D>())
        {
            collider2D.enabled = false;
        }
    }

    private void AnimDeath()
    {
        gameObject.GetComponentInChildren<Animator>().Play("Death");
    }

    public void DeathAnimationEnds()
    {
        Destroy(gameObject);
    }

    public void Deploy()
    {
        
    }

    public bool CheckTermsToDeploy()
    {

        return false;
    }

    public string GetElementColorString(int index)
    {
        return Element[index].colorString;
    }

    public string GetElementColorString(Gist gist)
    {
        return GetElementColorString(S.GetIndexByGist(gist));
    }

    public int GetElementManaPrice(int index)
    {
        return Element[index].manaPrice;
    }

    public int GetElementManaPrice(Gist gist)
    {
        return GetElementManaPrice(S.GetIndexByGist(gist));
    }
}
