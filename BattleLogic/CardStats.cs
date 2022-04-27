using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    private Stats stats;
    public GameObject cardUnit;
    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private Text[] ManaText = new Text[S.GistsCount];

    //Возможно когда-то у карт будут разные уровни спавна на поле
    //private int DeployLevel = 1;

    public void Start()
    {
        cardUnit = Instantiate(cardUnit);
        stats = cardUnit.GetComponent<Stats>();
        
        stats.SetUnitActivity(false);
        stats.SetConflictSide(ConflictSide.Player);
        stats.SetVisualState(Stats.Visual.Off);
        
        cardImage.sprite = stats.GetCardImage();

        for (int i = 0; i < S.GistsCount; i++)
        {
            if (ManaText[i] != null && stats.GetElementManaPrice(i) != 0)
            {
                ManaText[i].text = $"<color={stats.GetElementColorString(i)}>{stats.GetElementManaPrice(i)}</color>";
            }
            else
            {
                ManaText[i].gameObject.SetActive(false);
            }
        }
    }
}
