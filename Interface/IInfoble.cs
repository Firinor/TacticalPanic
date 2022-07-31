using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public interface IInfoble
    {
        public Sprite SpriteInfo { get; }

        public GistBasis[] GistBasis { get; }

        public string GetTextInfo();
    }
}
