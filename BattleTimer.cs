using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _manaBar = new GameObject[4];

    [SerializeField]
    private GameObject _missionObjectives;
    [SerializeField]
    private GameObject _goldCoins;

    private Slider[] _Slider = new Slider[4];

    private Text[] _SliderText = new Text[4];

    private Text _goldCoinsText;
    private Text _missionObjectivesText;

    void Start()
    {
        for (int i = 0; i < _manaBar.Length; i++)
        {
            if (_manaBar[i] != null) 
            {
                _Slider[i] = _manaBar[i].GetComponent<Slider>();
                _Slider[i].maxValue = SceneStats.MaxMana[i];
                _SliderText[i] = _manaBar[i].GetComponentInChildren<Text>();
                _SliderText[i].text = "0/" + SceneStats.MaxMana[i] + " +" + SceneStats.RegenMana[i]  + "/s";
            }
        }

        _goldCoinsText = _goldCoins.GetComponentInChildren<Text>();
        _missionObjectivesText = _missionObjectives.GetComponentInChildren<Text>();

        _goldCoinsText.text = "" + SceneStats.CurrentGold;
        _missionObjectivesText.text = "" + SceneStats.CurrentSityHealth + " / " + SceneStats.MaxSityHealth;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _Slider.Length; i++)
        {
            if (_Slider[i] != null)
            {
                SceneStats.CurrentMana[i] += SceneStats.RegenMana[i] * Time.fixedDeltaTime;
                _Slider[i].value = (int)SceneStats.CurrentMana[i];
                if (SceneStats.CurrentMana[i] >= SceneStats.MaxMana[i])
                {
                    _Slider[i].value = 0;
                    SceneStats.CurrentMana[i] = 0;
                }
                _SliderText[i].text = "" + _Slider[i].value + "/" + SceneStats.MaxMana[i] + " +" + SceneStats.RegenMana[i] + "/s";
            }
        }

        _goldCoinsText.text = "" + SceneStats.CurrentGold + "g";
        _missionObjectivesText.text = "" + SceneStats.CurrentSityHealth + " / " + SceneStats.MaxSityHealth;
    }
}
