using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    private Stats _stats;
    public GameObject _cardUnit;
    [SerializeField]
    private Text[] ManaText = new Text[4];

    //Возможно когда-то у карт будут разные уровни спавна на уровень
    private int DeployLevel = 1;

    public void Start()
    {
        _cardUnit = Instantiate(_cardUnit);
        _cardUnit.SetActive(false);
        _stats = _cardUnit.GetComponent<Stats>();

        for(int i = 0; i < 4; i++)
        {
            if (ManaText[i] != null && _stats.ManaPrice[i] != 0)
            {
                ManaText[i].text = $"<color={S.Мана[i].ColorString}>{_stats.ManaPrice[i]}</color>";
            }
            else
            {
                ManaText[i].gameObject.SetActive(false);
            }
        }
    }
}
