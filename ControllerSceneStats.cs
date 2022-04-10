using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSceneStats : MonoBehaviour
{
    public int[] MaxMana = new int[4];
    public float[] CurrentMana = new float[4];
    public float[] RegenMana = new float[4];
    [Space]
    public float[] HealPointPower = new float[4];
    public float[] DestroyPointPower = new float[4];
    [Space]
    [Header("Mission variables")]
    public int MaxSityHealth = 6;
    public int CurrentSityHealth = 6;
    [Space]
    public int CurrentGold = 0;

    void Awake()
    {
        for (int i = 0; i < MaxMana.Length; i++)
        {
            SceneStats.MaxMana[i] = MaxMana[i];
            SceneStats.CurrentMana[i] = CurrentMana[i];
            SceneStats.RegenMana[i] = RegenMana[i];
            SceneStats.HealPointPower[i] = -HealPointPower[i];
            SceneStats.DestroyPointPower[i] = DestroyPointPower[i];
        }

        SceneStats.MaxSityHealth = MaxSityHealth;
        SceneStats.CurrentSityHealth = CurrentSityHealth;
        SceneStats.CurrentGold = CurrentGold;
    }
}
