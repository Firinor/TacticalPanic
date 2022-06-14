using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class S // Top-Manager
{
    public static void OnLoad()
    {
        party = new List<UnitBasis>();
        account = SaveManager.Data.Account;
        UnitsCardManager.CreateUnits();
    }

    public static void AddUnitToParty(UnitBasis unit)
    {
        if(unit == null && !party.Contains(unit))
            party.Add(unit);
    }

    public static void RemoveUnitToParty(UnitBasis unit)
    {
        party.Remove(unit);
    }

    public static int[] GetPartyAsInts()
    {
        int[] result = new int[party.Count];
        for (int i = 0; i < result.Length; i++)
            result[i] = party[i].id;
        return result;
    }

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
        float d = Time.fixedDeltaTime;
        _cursorDamage = new float[]{HM.DestroyPower * d, MM.DestroyPower * d, EM.DestroyPower * d, SM.DestroyPower * d };
        _cursorHeal = new float[] {HM.HealPower * d, MM.HealPower * d, EM.HealPower * d, SM.HealPower * d };
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
        return GetCurrentMana(GetIndexByGist(gist));
    }

    public static float[] GetAllCurrentMana()
    {
        return new float[] { Mana[0].CurrentMana, Mana[1].CurrentMana, Mana[2].CurrentMana, Mana[3].CurrentMana };
    }

    public static Color GetManaColor(int index)
    {
        return Mana[index].Color;
    }

    public static Color GetManaColor(Gist gist)
    {
        return GetManaColor(GetIndexByGist(gist));
    }

    public static void DrawMana(int[] manaPrice)
    {
        for(int i = 0; i < manaPrice.Length && i < GistsCount; i++)
        {
            if(manaPrice[i] != 0)
                Mana[i].CurrentMana -= manaPrice[i];
        }
        TimeManager.RefreshBottleBar();
    }

    public static float ManaRegeneration(int index, float deltaTime)
    {
        Mana[index].CurrentMana += Mana[index].Regen * deltaTime;
        return Math.Min(Mana[index].CurrentMana, Mana[index].MaxMana);
    }

    public static float ManaRegeneration(Gist gist, float deltaTime)
    {
        return ManaRegeneration(GetIndexByGist(gist), deltaTime);
    }

    public static void ManaRegeneration(float deltaTime)
    {
        for (int i = 0; i < GistsCount; i++)
        {
            ManaRegeneration(i, deltaTime);
        }
    }

    public static TimeManager GetBattleTimer()
    {
        if (battleTimer == null)
        {
            battleTimer = GameObject.Find("GameController").GetComponent<TimeManager>();
        }

        return battleTimer;
    }

    public static CursorOperator GetCursorManager()
    {
        if (cursorManager == null)
        {
            cursorManager = GameObject.Find("GameController").GetComponent<CursorOperator>();
        }

        return cursorManager;
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