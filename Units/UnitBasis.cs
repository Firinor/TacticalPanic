using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasis
{
    public int id { get; private set; }
    public string unitName { get; private set; }
    public float mspeed { get; private set; }
    public BodyGist[] bodyGists { get; private set; }

    private UnitSprite unitSprite;
    public Sprite SpriteInfo => throw new System.NotImplementedException();

    public BodyElement[] Elements => throw new System.NotImplementedException();

    public UnitBasis(int id, string unitName, float mspeed, BodyGist[] bodyGists)
    {
        this.id = id;
        this.unitName = unitName;
        this.mspeed = mspeed;
        this.bodyGists = bodyGists;
    }

    public string GetTextInfo()
    {
        throw new System.NotImplementedException();
    }

    public void SetUnitSprite(UnitSprite unitSprite)
    {
        this.unitSprite = unitSprite;
    }
}
