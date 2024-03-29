using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalPanicCode
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] manaBar = new GameObject[PlayerOperator.GistsCount];

        [SerializeField]
        private GameObject missionObjectives;

        private Slider[] slider = new Slider[PlayerOperator.GistsCount];
        [SerializeField]
        private static Slider gameSpeedSlider;

        private Text[] sliderText = new Text[PlayerOperator.GistsCount];

        private Text missionObjectivesText;

        public void Start()
        {
            for (int i = 0; i < PlayerOperator.GistsCount; i++)
            {
                if (manaBar[i] != null)
                {
                    slider[i] = manaBar[i].GetComponent<Slider>();
                    slider[i].maxValue = PlayerOperator.GetMaxMana(i);
                    sliderText[i] = manaBar[i].GetComponentInChildren<Text>();
                    sliderText[i].text = "" + slider[i].value + "/" + slider[i].maxValue + " +" + PlayerOperator.GetRegen(i);
                }
            }

            missionObjectivesText = missionObjectives.GetComponentInChildren<Text>();
            missionObjectivesText.text = "" + PlayerOperator.CurrentSityHealth + " / " + PlayerOperator.MaxSityHealth;

            gameSpeedSlider = GameObject.Find("GameSpeedSlider").GetComponent<Slider>();

            foreach (EnemySquadsInformator enemy in PlayerManager.PickedLevel.enemies)
            {
                StartCoroutine(enemy.Start());
            }
        }

        public void FixedUpdate()
        {

            float delta = Time.deltaTime;

            for (int i = 0; i < PlayerOperator.GistsCount; i++)
            {
                if (slider[i] != null)
                {
                    float NewValue = PlayerOperator.ManaRegeneration(i, delta);
                    if ((int)NewValue != slider[i].value)
                    {
                        RefreshBottleBar(i);
                    }
                }
            }
        }

        public static void RefreshBottleBar()
        {
            for (int i = 0; i < PlayerOperator.GistsCount; i++)
            {
                RefreshBottleBar(i);
            }
        }

        public static void RefreshBottleBar(int i)
        {
            TimeManager B = PlayerOperator.GetBattleTimer();
            B.slider[i].value = PlayerOperator.GetCurrentMana(i);
            B.sliderText[i].text = "" + B.slider[i].value + "/" + PlayerOperator.GetMaxMana(i) + " +" + PlayerOperator.GetRegen(i);
        }

        public static void SetGameSpeed(float newSpeed)
        {
            Time.timeScale = newSpeed;

            gameSpeedSlider.value = newSpeed;
        }
    }
}
