using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneStats
{
    public static int MaxBlueMana {get;set;} = 1000;
    public static int MaxRedMana { get; set; } = 1500;
    public static int MaxGreenMana { get; set; } = 1000;
    public static int MaxYellowMana { get; set; } = 1500;

    public static float CurrentBlueMana { get; set; } = 0;
    public static float CurrentRedMana { get; set; } = 0;
    public static float CurrentGreenMana { get; set; } = 0;
    public static float CurrentYellowMana { get; set; } = 0;

    public static float RegenBlueMana { get; set; } = 1;
    public static float RegenRedMana { get; set; } = 2;
    public static float RegenGreenMana { get; set; } = 1;
    public static float RegenYellowMana { get; set; } = 2;

    public static int MaxSityHealth { get; set; } = 6;
    public static int CurrentSityHealth { get; set; } = 6;

    public static float HPHealPower { get; set; } = .2f;
    public static float HPDestroyPower { get; set; } = .2f;
    public static float MPHealPower { get; set; } = .2f;
    public static float MPDestroyPower { get; set; } = .2f;
    public static float CPHealPower { get; set; } = .2f;
    public static float CPDestroyPower { get; set; } = .2f;
    public static float SPHealPower { get; set; } = .2f;
    public static float SPDestroyPower { get; set; } = .2f;

}
