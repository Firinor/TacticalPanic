using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Points { HP, MP, CP, SP };

public class S : MonoBehaviour
{
    [SerializeField]
    public int[] maxMana = new int[4];
    
    private static float[] currentMana = new float[4];
    public static float[] CurrentMana
    {
        get => currentMana; 
        set 
        {
            for (int i = 0; i < value.Length; i++)
                currentMana[i] = Math.Min(value[i], MaxMana[i]);
            currentMana = value; 
        }
    }


    public float[] regenMana = new float[4];
    [Space]
    public float[] healPointPower = new float[4];
    public float[] destroyPointPower = new float[4];
    [Space]
    [Header("Mission variables")]
    public int maxSityHealth = 6;
    public int currentSityHealth = 6;
    [Space]
    public int currentGold = 0;

    public static int[] MaxMana = new int[4];
    //public static float[] CurrentMana = new float[4];
    public static float[] RegenMana = new float[4];
    [Space]
    public static float[] HealPointPower = new float[4];
    public static float[] DestroyPointPower = new float[4];
    [Space]
    [Header("Mission variables")]
    public static int MaxSityHealth = 6;
    public static int CurrentSityHealth = 6;
    [Space]
    public static int CurrentGold = 0;
    
    public static readonly string[] ColorString = new string[4] { "red", "#0088ff", "yellow", "lime" };

    //public S()
    //{
    //    currentMana = new float[4];
    //}
    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            MaxMana[i] = maxMana[i];
            //CurrentMana[i] = _currentMana[i];
            RegenMana[i] = regenMana[i];
            DestroyPointPower[i] = destroyPointPower[i];
            HealPointPower[i] = healPointPower[i];
        }

        MaxSityHealth = maxSityHealth;
        CurrentSityHealth = currentSityHealth;
        CurrentGold = currentGold;
    }
}
