using UnityEngine;

namespace TacticalPanicCode
{
    public enum Gist
    {
        Life = GistColors.Red,
        Magic = GistColors.Blue,
        Energy = GistColors.Yellow,
        Spectrum = GistColors.Green,
        Aura = GistColors.Orange,
        Corruption = GistColors.Purple,
        Boredom = GistColors.Gray
    };

    public enum GistColors { Red, Blue, Yellow, Green, Orange, Purple, Gray }

    public static class GistColorInformator
    {
        public struct Facts
        {
            public Facts(string TextColor, Color UnityColor)
            {
                this.TextColor = TextColor;
                this.UnityColor = UnityColor;
            }

            public string TextColor { get; }
            public Color UnityColor { get; }
        }

        public static readonly Facts Red = new Facts("red", Color.red);
        public static readonly Facts Blue = new Facts("blue", Color.blue);
        public static readonly Facts Yellow = new Facts("yellow", Color.yellow);
        public static readonly Facts Green = new Facts("green", Color.green);
        public static readonly Facts Orange = new Facts("orange", new Color(1, 0.5f, 0f, 1));
        public static readonly Facts Purple = new Facts("purple", Color.magenta);
        public static readonly Facts Gray = new Facts("gray", Color.gray);

        public static Facts ColorByIndex(int index)
        {
            return index switch
            {
                0 => Red,
                1 => Blue,
                2 => Yellow,
                3 => Green,
                4 => Orange,
                5 => Purple,
                6 => Gray,
                _ => Red,
            };
        }
    }
}