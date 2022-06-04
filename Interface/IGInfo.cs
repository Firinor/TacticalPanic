using UnityEngine;
using UnityEngine.UI;

public interface IGInfo
{
    public Sprite SpriteInfo { get; }

    public BodyGist[] Elements { get; }

    public string GetTextInfo();
}
