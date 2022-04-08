using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject _greenMana;
    [SerializeField]
    private GameObject _blueMana;
    [SerializeField]
    private GameObject _missionObjectives;
    [SerializeField]
    private GameObject _goldCoins;

    private Slider _greenSlider;
    private Slider _blueSlider;

    private Text _greenSliderText;
    private Text _blueSliderText;

    

    void Start()
    {
        _greenSlider = _greenMana.GetComponent<Slider>();
        _blueSlider = _blueMana.GetComponent<Slider>();

        _greenSlider.maxValue = SceneStats.MaxGreenMana;
        _blueSlider.maxValue = SceneStats.MaxBlueMana;

        _greenSliderText = _greenMana.GetComponentInChildren<Text>();
        _blueSliderText = _blueMana.GetComponentInChildren<Text>();

        _greenSliderText.text = "0/" + SceneStats.MaxGreenMana;
        _blueSliderText.text = "0/" + SceneStats.MaxBlueMana;
    }

    void FixedUpdate()
    {
        SceneStats.CurrentGreenMana += SceneStats.RegenGreenMana;
        SceneStats.CurrentBlueMana += SceneStats.RegenBlueMana;

        _greenSlider.value = (int)SceneStats.CurrentGreenMana;
        _blueSlider.value = (int)SceneStats.CurrentBlueMana;

        if (SceneStats.CurrentGreenMana >= SceneStats.MaxGreenMana)
        {
            _greenSlider.value = 0;
            SceneStats.CurrentGreenMana = 0;
        }

        if (SceneStats.CurrentBlueMana >= SceneStats.MaxBlueMana)
        {
            _blueSlider.value = 0;
            SceneStats.CurrentBlueMana = 0;
        }

        _greenSliderText.text = "" + SceneStats.CurrentGreenMana + "/" + SceneStats.MaxGreenMana;
        _blueSliderText.text = "" + SceneStats.CurrentBlueMana + "/" + SceneStats.MaxBlueMana;
    }
}
