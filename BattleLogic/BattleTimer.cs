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
                _Slider[i].maxValue = S.Ìàíà[i].MaxMana;
                _SliderText[i] = _manaBar[i].GetComponentInChildren<Text>();
                _SliderText[i].text = "0/" + S.Ìàíà[i].MaxMana + " +" + S.Ìàíà[i].RegenMana  + "/s";
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
                if (S.Ìàíà[i].CurrentMana < S.Ìàíà[i].MaxMana) 
                {
                    S.Ìàíà[i].CurrentMana += S.Ìàíà[i].RegenMana * Time.fixedDeltaTime;
                    _Slider[i].value = (int)S.Ìàíà[i].CurrentMana;
                    _SliderText[i].text = "" + _Slider[i].value + "/" + S.Ìàíà[i].MaxMana + " +" + S.Ìàíà[i].RegenMana + "/s";
                }
            }
        }

        _goldCoinsText.text = "" + S.CurrentGold + "g";
        _missionObjectivesText.text = "" + S.CurrentSityHealth + " / " + S.MaxSityHealth;
    }
}
