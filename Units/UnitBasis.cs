using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasis: IGInfo
{
    public int id { get; private set; }
    public string unitName { get; private set; }
    public float mspeed { get; private set; }
    public BodyGist[] bodyGists { get; private set; }

    private UnitSprite unitSprite;
    public Sprite SpriteInfo => unitSprite.unitSprite;

    public BodyElement[] Elements => new BodyElement[0];

    public UnitBasis(int id, string unitName, float mspeed, BodyGist[] bodyGists)
    {
        this.id = id;
        this.unitName = unitName;
        this.mspeed = mspeed;
        this.bodyGists = bodyGists;
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
