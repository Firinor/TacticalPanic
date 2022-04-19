using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Points { HP, MP, CP, SP };
//public static readonly string[] ColorString = new string[4] { "red", "#0088ff", "yellow", "lime" };

[Serializable]
public struct ManaStruct
{
    public string ColorString;
    public int MaxMana;
    public float CurrentMana;
    public float RegenMana;
    public float HealPointPower;
    public float DestroyPointPower;

    public ManaStruct(int maxMana = 100, float currentMana = 0, float regenMana = 1, string colorString = "", 
        float healPointPower = 0, float destroyPointPower = 0)
    {
        MaxMana = maxMana;
        CurrentMana = currentMana;
        RegenMana = regenMana;
        ColorString = colorString;
        HealPointPower = healPointPower;
        DestroyPointPower = destroyPointPower;
    }
}

public class S : MonoBehaviour
{
    [SerializeField]
    public ManaStruct[] manaStruct = new ManaStruct[4];
    [Space]
    [Header("Mission variables")]
    public int maxSityHealth = 6;
    public int currentSityHealth = 6;
    [Space]
    public int currentGold = 0;

    public static ManaStruct[] Mana = new ManaStruct[4];
    public static int MaxSityHealth = 6;
    public static int CurrentSityHealth = 6;
    public static int CurrentGold = 0;

    public ManaStruct this[int index]
    {
        get { return Mana[index]; }
        set { Mana[index] = value; }
    }

    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Mana[i] = manaStruct[i];
        }

        MaxSityHealth = maxSityHealth;
        CurrentSityHealth = currentSityHealth;
        CurrentGold = currentGold;
    }
}