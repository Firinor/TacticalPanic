using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Points { HP, MP, CP, SP };
//public static readonly string[] ColorString = new string[4] { "red", "#0088ff", "yellow", "lime" };

public class Mana
{
    public string Name;
    public Points Point;
    public string ColorString;
    public int MaxMana;
    public float CurrentMana;
    //{
    //    get { return CurrentMana; }
    //    set => CurrentMana = value > MaxMana ? MaxMana : value;
    //}
    public float RegenMana;
    public float HealPointPower;
    public float DestroyPointPower;
    

    public Mana(string name = "", string colorString = "", Points point = Points.HP,
        int maxMana = 100, float currentMana = 0, float regenMana = 1,
        float healPointPower = 0, float destroyPointPower = 0)
    {
        Name = name;
        MaxMana = maxMana;
        CurrentMana = currentMana;
        RegenMana = regenMana;
        ColorString = colorString;
        Point = point;
        HealPointPower = healPointPower;
        DestroyPointPower = destroyPointPower;
    }
}

public static class S
{
    //[SerializeField]
    public static Mana HP = new Mana("Health points", "red", Points.HP, 50, 0, 1, 1, 1);
    public static Mana MP = new Mana("Magic points", "#0088ff", Points.MP, 50, 0, 1, 1, 1);
    public static Mana CP = new Mana("CP", "yellow", Points.CP, 50, 0, 1, 1, 1);
    public static Mana SP = new Mana("SP", "lime", Points.SP, 50, 0, 1, 1, 1);
    public static Mana[] Мана = { HP, MP, CP, SP };

    public static int MaxSityHealth = 6;
    public static int CurrentSityHealth = 6;
    public static int CurrentGold = 0;

    public static int GetIndexByPoint(Points _point)
    {
        return _point switch
        {
            Points.HP => 0,
            Points.MP => 1,
            Points.CP => 2,
            Points.SP => 3,
            _ => -1,
        };
    }
}