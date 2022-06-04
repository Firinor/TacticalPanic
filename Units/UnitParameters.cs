using UnityEngine;

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

public static class G
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
    public static readonly Facts Yellow = new Facts("green", Color.green);
    public static readonly Facts Green = new Facts("yellow", Color.yellow);
    public static readonly Facts Orange = new Facts("orange", new Color(1,0.5f,0f,1));
    public static readonly Facts Purple = new Facts("purple", Color.magenta);
    public static readonly Facts Gray = new Facts("gray", Color.gray);
}

public class BodyGist
{
    public const int Count = 7;

    public Gist gist;

    public int points;
    public int attack;
    public int defense;
    public float reattack;
    public float regen;
    public float moveSpeed;

    public int manaPrice;
}