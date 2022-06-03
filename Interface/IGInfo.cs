using UnityEngine;
using UnityEngine.UI;

public interface IGInfo
{
    public Sprite SpriteInfo { get; }

    public BodyElement[] Elements { get; }

    public string GetTextInfo();
}
