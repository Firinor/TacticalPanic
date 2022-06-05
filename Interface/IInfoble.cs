using UnityEngine;
using UnityEngine.UI;

public interface IInfoble
{
    public Sprite SpriteInfo { get; }

    public BodyGist[] Elements { get; }

    public string GetTextInfo();
}
