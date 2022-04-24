using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] manaBar = new GameObject[S.GistsCount];

    [SerializeField]
    private GameObject missionObjectives;
    [SerializeField]
    private GameObject goldCoins;

    private Slider[] slider = new Slider[S.GistsCount];

    private Text[] sliderText = new Text[S.GistsCount];

    private Text goldCoinsText;
    private Text missionObjectivesText;

    private static float GameSpeed;
    public static float gameSpeed
    {
        get { return GameSpeed; }
        set { GameSpeed = Math.Max(value, 0f); }
    }


    void Start()
    {
        GameSpeed = 1;

        for (int i = 0; i < S.GistsCount; i++)
        {
            if (manaBar[i] != null) 
            {
                slider[i] = manaBar[i].GetComponent<Slider>();
                slider[i].maxValue = S.GetMaxMana(i);
                sliderText[i] = manaBar[i].GetComponentInChildren<Text>();
                sliderText[i].text = "" + slider[i].value + "/" + slider[i].maxValue + " +" + S.GetRegen(i);
            }
        }

        goldCoinsText = goldCoins.GetComponentInChildren<Text>();
        missionObjectivesText = missionObjectives.GetComponentInChildren<Text>();

        goldCoinsText.text = "" + S.CurrentGold;
        missionObjectivesText.text = "" + S.CurrentSityHealth + " / " + S.MaxSityHealth;
    }

    void FixedUpdate()
    {
        if (GameSpeed == 0)
            return;

        float delta = gameSpeed * Time.deltaTime;

        for (int i = 0; i < S.GistsCount; i++)
        {
            if (slider[i] != null)
            {
                float NewValue = S.ManaRegeneration(i, delta);
                if ((int)NewValue != slider[i].value)
                {
                    RefreshBottleBar(i);
                }
            }
        }
    }

    public static void RefreshBottleBar()
    {
        for (int i = 0; i < S.GistsCount; i++)
        {
            RefreshBottleBar(i);
        }
    }

    public static void RefreshBottleBar(int i)
    {
        BattleTimer B = S.GetBattleTimer();
        B.slider[i].value = S.GetCurrentMana(i);
        B.sliderText[i].text = "" + B.slider[i].value + "/" + S.GetMaxMana(i) + " +" + S.GetRegen(i);
    }

    public static void SetGameSpeed(float newSpeed)
    {
        gameSpeed = newSpeed;
    }
}
