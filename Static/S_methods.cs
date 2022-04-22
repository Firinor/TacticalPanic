using System;
using UnityEngine;

public static partial class S
{
    public static int GetIndexByGist(Gist gist)
    {
        return gist switch
        {
            Gist.Life => 0,
            Gist.Magic => 1,
            Gist.Energy => 2,
            Gist.Spectrum => 3,
            _ => -1,
        };
    }

    public static void GetCursorMagic(out float[] _cursorDamage, out float[] _cursorHeal)
    {
        _cursorDamage = new float[]{HM.DestroyPower, MM.DestroyPower, EM.DestroyPower, SM.DestroyPower };
        _cursorHeal = new float[] {HM.HealPower, MM.HealPower, EM.HealPower, SM.HealPower };
    }

    public static void GetBottleBar(out float[] _cursorDamage, out float[] _cursorHeal)
    {
        _cursorDamage = new float[] { HM.DestroyPower, MM.DestroyPower, EM.DestroyPower, SM.DestroyPower };
        _cursorHeal = new float[] { HM.HealPower, MM.HealPower, EM.HealPower, SM.HealPower };
    }

    public static int GetMaxMana(int index)
    {
        return Mana[index].MaxMana;
    }

    public static int GetMaxMana(Gist gist)
    {
        return GetMaxMana(GetIndexByGist(gist));
    }

    public static float GetRegen(int index)
    {
        return Mana[index].Regen;
    }

    public static float GetRegen(Gist gist)
    {
        return GetRegen(GetIndexByGist(gist));
    }

    public static float GetCurrentMana(int index)
    {
        return Mana[index].CurrentMana;
    }

    public static float GetCurrentMana(Gist gist)
    {
        return GetRegen(GetIndexByGist(gist));
    }

    public static float SetCurrentMana(int index)
    {
        return Mana[index].CurrentMana;
    }

    public static float ManaRegeneration(int index)
    {
        Mana[index].CurrentMana += Mana[index].Regen;
        return Math.Min(Mana[index].CurrentMana, Mana[index].MaxMana);
    }

    public static float ManaRegeneration(Gist gist)
    {
        return ManaRegeneration(GetIndexByGist(gist));
    }

    public static void ManaRegeneration()
    {
        for (int i = 0; i < GistsCount; i++)
        {
            ManaRegeneration(i);
        }
    }
}

public static partial class SideColor
{
    public static Color ColorOfSide(ConflictSide side)
    {
        return side switch
        {
            ConflictSide.Player => player,
            ConflictSide.Neutral => neutral,
            ConflictSide.Peaceful => peaceful,
            /*ConflictSide.Enemy:*/
            _ => enemy
        };
    }
}