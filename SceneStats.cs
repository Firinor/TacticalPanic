using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneStats
{
    public static int[] MaxMana = new int[4];

    public static float[] CurrentMana = new float[4];

    public static float[] RegenMana = new float[4];

    public static int MaxSityHealth { get; set; } = 6;
    public static int CurrentSityHealth { get; set; } = 6;

    public static int CurrentGold { get; set; } = 0;

    public static float[] HealPointPower = new float[4];
    public static float[] DestroyPointPower = new float[4];

}
