using UnityEngine;
using UnityEngine.UI;

public interface IInfo 
{
    public Sprite SpriteInfo { get; }

    public BodyElement[] Elements { get; }

    public string GetTextInfo();
}
