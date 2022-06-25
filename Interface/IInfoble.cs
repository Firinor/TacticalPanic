using UnityEngine;
using UnityEngine.UI;

public interface IInfoble
{
    public Sprite SpriteInfo { get; }

    public GistBasis[] GistBasis { get; }

    public string GetTextInfo();
}
