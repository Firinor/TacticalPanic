using System;
using UnityEngine;

public enum Gist { Life, Magic, Energy, Spectrum };
public enum ConflictSide { Player, Neutral, Peaceful, Enemy };
public static partial class SideColor 
{
    public static readonly Color player = new Color(.38f, .45f, .92f, 1f);
    public static readonly Color neutral = new Color(.5f, .5f, .5f, 1f);
    public static readonly Color enemy = new Color(.92f, .29f, .15f, 1f);
    public static readonly Color peaceful = new Color(0f, 1f, 0f, 1f);
}

public static partial class S
{
    public static int GistsCount { get; } = Enum.GetValues(typeof(Gist)).Length;
    private static BattleTimer battleTimer;
    private static CursorManager cursorManager;

    private static ManaBar HP = new ManaBar("Health points", "red", Gist.Life, 50, 0, 1);
    private static ManaBar MP = new ManaBar("Mana points", "#0088ff", Gist.Magic, 50, 0, 1);
    private static ManaBar EP = new ManaBar("Energy points", "yellow", Gist.Energy, 50, 0, 1);
    private static ManaBar SP = new ManaBar("Special points", "lime", Gist.Spectrum, 50, 0, 1);
    private static ManaBar[] Mana = { HP, MP, EP, SP };

    private static MagicPower HM = new MagicPower(15, 1);
    private static MagicPower MM = new MagicPower(100, 1);
    private static MagicPower EM = new MagicPower(0, 1);
    private static MagicPower SM = new MagicPower(1, 1);
    private static MagicPower[] Magic = { HM, MM, EM, SM };

    public static int MaxSityHealth { get; } = 6;
    public static int CurrentSityHealth { get; } = 6;
    public static int CurrentGold { get; } = 0;

    private struct ManaBar
    {
        public string Name;
        public Gist Point;
        public string ColorString;
        public int MaxMana;
        public float CurrentMana;
        public float Regen;

        public ManaBar(string name = "", string colorString = "", Gist point = Gist.Life,
            int maxMana = 100, float currentMana = 0, float regen = 1)
        {
            Name = name;
            MaxMana = maxMana;
            CurrentMana = currentMana;
            Regen = regen;
            ColorString = colorString;
            Point = point;
        }
    }

    private class MagicPower
    {
        public float HealPower;
        public float DestroyPower;

        public MagicPower(float destroyPower = 0, float healPower = 0)
        {
            HealPower = healPower;
            DestroyPower = destroyPower;
        }
    }
}