using System;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    //            UnitInformator
    //UnitBasis <
    //            Unit -> UnitCard
    //
    // Gist -> GistBasis -> GistOfUnit
    public class UnitBasis : IInfoble
    {
        public int id { get; private set; }
        public string unitName { get; private set; }
        public float mspeed { get; private set; }
        public int blockCount { get; private set; }
        public float agroRadius { get; private set; }
        public float attackRadius { get; private set; }
        public Gist GistOfDeath { get; private set; }
        public UnitInformator unitInformator { get; set; }
        public Sprite SpriteInfo => unitInformator.unitSprite;
        public GistBasis[] GistBasis { get; }

        public UnitBasis(int id, string unitName, float mspeed, Gist DeathElement, GistBasis[] GistBasis)
        {
            this.id = id;
            this.unitName = unitName;
            this.mspeed = mspeed;
            this.GistOfDeath = DeathElement;
            this.GistBasis = GistBasis;
        }

        public string GetTextInfo()
        {
            return unitName;
        }
    }

    public class GistBasis
    {
        public const int Count = 7;

        public Gist gist;

        public int points;
        public int attack;
        public int defense;
        public float reattack;
        public float regen;
        public float moveSpeed;

        public int manaPrice;
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
}