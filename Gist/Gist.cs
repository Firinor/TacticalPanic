using System.Numerics;

//            UnitInformator
//UnitBasis <
//            UnitOperator,UnitStats,UnitSkills  -> UnitCard
//
// Gist -> GistBasis -> GistOfUnit
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
        public Facts(string TextColor, Vector4 RGBAColor)
        {
            this.TextColor = TextColor;
            this.RGBAColor = RGBAColor;
        }

        public string TextColor { get; }
        public Vector4 RGBAColor { get; }
    }

    public static readonly Facts Red = new Facts("red", new Vector4(1, 0, 0, 1));
    public static readonly Facts Blue = new Facts("blue", new Vector4(0, 0, 1, 1));
    public static readonly Facts Yellow = new Facts("yellow", new Vector4(1f, 47f / 51f, 4f / 255f, 1f));
    public static readonly Facts Green = new Facts("green", new Vector4(0, 1, 0, 1));
    public static readonly Facts Orange = new Facts("orange", new Vector4(1, 0.5f, 0f, 1));
    public static readonly Facts Purple = new Facts("purple", new Vector4(1, 0, 1, 1));
    public static readonly Facts Gray = new Facts("gray", new Vector4(0.5f, 0.5f, 0.5f, 1));

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