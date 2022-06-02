using UnityEngine;

public enum Gist { Life, Magic, Energy, Spectrum, Aura, Ñorruption, Boredom };
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
    public static readonly Facts Orange = new Facts("orange", new Color(1,1,1,1));
    public static readonly Facts Purple = new Facts("purple", Color.magenta);
    public static readonly Facts Gray = new Facts("gray", Color.gray);
}
