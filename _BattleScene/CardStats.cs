using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    private Unit stats;
    [SerializeField]
    private GameObject unitPrefab;
    [SerializeField]
    private Image cardImage;

    [SerializeField]
    private Text[] ManaText = new Text[PlayerOperator.GistsCount];

    //Возможно когда-то у карт будут разные уровни спавна на поле
    //private int DeployLevel = 1;

    public void Start()
    {
        unitPrefab = Instantiate(unitPrefab);
        stats = unitPrefab.GetComponent<Unit>();
        
        stats.SetUnitActivity(false);
        stats.SetConflictSide(ConflictSide.Player);
        stats.SetVisualState(VisualOfUnit.Off);

        cardImage.sprite = stats.GetCardSprite();

        for (int i = 0; i < PlayerOperator.GistsCount; i++)
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

    public void SetCardUnit(UnitBasis unit)
    {
        //DB.Units.
    }

    public GameObject GetUnitPrefab()
    {
        return unitPrefab;
    }
}
