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

    private float gameSpeed = 1;

    void Start()
    {
        for (int i = 0; i < S.GistsCount; i++)
        {
            if (manaBar[i] != null) 
            {
                slider[i] = manaBar[i].GetComponent<Slider>();
                slider[i].maxValue = S.GetMaxMana(i);
                sliderText[i] = manaBar[i].GetComponentInChildren<Text>();
                sliderText[i].text = "0/" + slider[i].maxValue + " +" + S.GetRegen(i) + "/s";
            }
        }

        goldCoinsText = goldCoins.GetComponentInChildren<Text>();
        missionObjectivesText = missionObjectives.GetComponentInChildren<Text>();

        goldCoinsText.text = "" + S.CurrentGold;
        missionObjectivesText.text = "" + S.CurrentSityHealth + " / " + S.MaxSityHealth;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < S.GistsCount; i++)
        {
            if (slider[i] != null)
            {
                float NewValue = S.ManaRegeneration(i);
                if ((int)NewValue == slider[i].value)
                {
                    slider[i].value = NewValue;
                    sliderText[i].text = "" + slider[i].value + "/" + S.GetMaxMana(i) + " +" + S.GetRegen(i) + "/s";
                }
            }
        }
    }
}
