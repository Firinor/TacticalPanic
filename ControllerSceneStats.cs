using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSceneStats : MonoBehaviour
{
    public int MaxBlueMana = 1000;
    public int MaxRedMana = 1500;
    public int MaxGreenMana = 1000;
    public int MaxYellowMana = 1500;

    public float CurrentBlueMana = 0;
    public float CurrentRedMana = 0;
    public float CurrentGreenMana = 0;
    public float CurrentYellowMana = 0;

    public float RegenBlueMana = 1;
    public float RegenRedMana = 2;
    public float RegenGreenMana = 1;
    public float RegenYellowMana = 2;

    public float HPHealPower = 1;
    public float HPDestroyPower = 2;
    public float MPHealPower = 1;
    public float MPDestroyPower = 2;
    public float CPHealPower = 1;
    public float CPDestroyPower = 2;
    public float SPHealPower = 1;
    public float SPDestroyPower = 2;

    public int MaxSityHealth = 6;
    public int CurrentSityHealth = 6;

    void Awake()
    {
        SceneStats.MaxBlueMana = MaxBlueMana;
        SceneStats.MaxRedMana = MaxRedMana;
        SceneStats.MaxGreenMana = MaxGreenMana;
        SceneStats.MaxYellowMana = MaxYellowMana;

        SceneStats.CurrentBlueMana = CurrentBlueMana;
        SceneStats.CurrentRedMana = CurrentRedMana;
        SceneStats.CurrentGreenMana = CurrentGreenMana;
        SceneStats.CurrentYellowMana = CurrentYellowMana;

        SceneStats.RegenBlueMana = RegenBlueMana;
        SceneStats.RegenRedMana = RegenRedMana;
        SceneStats.RegenGreenMana = RegenGreenMana;
        SceneStats.RegenYellowMana = RegenYellowMana;

        SceneStats.HPHealPower = -HPHealPower;
        SceneStats.HPDestroyPower = HPDestroyPower;
        SceneStats.MPHealPower = -MPHealPower;
        SceneStats.MPDestroyPower = MPDestroyPower;
        SceneStats.CPHealPower = -CPHealPower;
        SceneStats.CPDestroyPower = CPDestroyPower;
        SceneStats.SPHealPower = -SPHealPower;
        SceneStats.SPDestroyPower = SPDestroyPower;

        SceneStats.MaxSityHealth = MaxSityHealth;
        SceneStats.CurrentSityHealth = CurrentSityHealth;
    }
}
