using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasis : ScriptableObject, IInfo
{
    public Sprite SpriteInfo => throw new System.NotImplementedException();

    public BodyElement[] Elements => throw new System.NotImplementedException();

    public string GetTextInfo()
    {
        throw new System.NotImplementedException();
    }
}
