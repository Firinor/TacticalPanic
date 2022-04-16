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
                _Slider[i].maxValue = S.MaxMana[i];
                _SliderText[i] = _manaBar[i].GetComponentInChildren<Text>();
                _SliderText[i].text = "0/" + S.MaxMana[i] + " +" + S.RegenMana[i]  + "/s";
            }
        }

        _goldCoinsText = _goldCoins.GetComponentInChildren<Text>();
        _missionObjectivesText = _missionObjectives.GetComponentInChildren<Text>();

        _goldCoinsText.text = "" + S.CurrentGold;
        _missionObjectivesText.text = "" + S.CurrentSityHealth + " / " + S.MaxSityHealth;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _manaBar.Length; i++)
        {
            if (_Slider[i] != null)
            {
                S.CurrentMana[i] += S.RegenMana[i] * Time.fixedDeltaTime;
                _Slider[i].value = (int)S.CurrentMana[i];
                if (S.CurrentMana[i] >= S.MaxMana[i])
                {
                    _Slider[i].value = 0;
                    S.CurrentMana[i] = 0;
                }
                _SliderText[i].text = "" + _Slider[i].value + "/" + S.MaxMana[i] + " +" + S.RegenMana[i] + "/s";
            }
        }

        _goldCoinsText.text = "" + S.CurrentGold + "g";
        _missionObjectivesText.text = "" + S.CurrentSityHealth + " / " + S.MaxSityHealth;
    }
}
