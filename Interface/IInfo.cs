using UnityEngine;
using UnityEngine.UI;

public interface IInfo 
{
    public Sprite SpriteInfo { get; }

    public Numerical NumericalInfo { get; }

    public string GetTextInfo();
}
