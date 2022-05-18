using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInformator : MonoBehaviour
{
    [SerializeField]
    private GameObject baner;
    [SerializeField]
    private GameObject credits;
    [SerializeField]
    private GameObject saves;
    private GameObject options;
    public static MainMenuInformator instance { get; private set; }

    void Awake()
    {
        instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainMenuInformator>();
    }

    public static GameObject GetBaner()
    {
        return instance.baner;
    }

    public static GameObject GetCredits()
    {
        return instance.credits;
    }

    public static GameObject GetSaves()
    {
        return instance.saves;
    }


}
