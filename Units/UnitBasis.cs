using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasis: IGInfo
{
    public int id { get; private set; }
    public string unitName { get; private set; }
    public float mspeed { get; private set; }

    private UnitSprite unitSprite;
    public Sprite SpriteInfo => unitSprite.unitSprite;

    public BodyElement[] bodyElements => new BodyElement[0];

    public BodyGist[] Elements { get; }

    public UnitBasis(int id, string unitName, float mspeed, BodyGist[] Elements)
    {
        this.id = id;
        this.unitName = unitName;
        this.mspeed = mspeed;
        this.Elements = Elements;
    }

    public string GetTextInfo()
    {
        return unitName;
    }

    public void SetUnitSprite(UnitSprite unitSprite)
    {
        this.unitSprite = unitSprite;
    }

    public UnitSprite GetUnitSprite()
    {
        return unitSprite;
    }
}
