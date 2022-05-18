using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] manaBar = new GameObject[S.GistsCount];

    [SerializeField]
    private GameObject missionObjectives;
    [SerializeField]
    private GameObject goldCoins;

    private Slider[] slider = new Slider[S.GistsCount];
    [SerializeField]
    private static Slider gameSpeedSlider;

    private Text[] sliderText = new Text[S.GistsCount];

    private Text goldCoinsText;
    private Text missionObjectivesText;

    public void Start()
    {
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

        gameSpeedSlider = GameObject.Find("GameSpeedSlider").GetComponent<Slider>();
    }

    public void FixedUpdate()
    {

        float delta = Time.deltaTime;

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
        TimeManager B = S.GetBattleTimer();
        B.slider[i].value = S.GetCurrentMana(i);
        B.sliderText[i].text = "" + B.slider[i].value + "/" + S.GetMaxMana(i) + " +" + S.GetRegen(i);
    }

    public static void SetGameSpeed(float newSpeed)
    {
        Time.timeScale = newSpeed;

        gameSpeedSlider.value = newSpeed;
    }
}
