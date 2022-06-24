using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    private Unit unit;
    [SerializeField]
    private Image cardImage;
    [SerializeField]
    private GameObject unitPrefab;

    [SerializeField]
    private Text[] ManaText = new Text[PlayerOperator.GistsCount];

    public void Start()
    {
        unit.SetUnitActivity(false);
        unit.SetConflictSide(ConflictSide.Player);
        unit.SetVisualState(VisualOfUnit.Off);

        cardImage.sprite = unit.GetCardSprite();

        for (int i = 0; i < PlayerOperator.GistsCount; i++)
        {
            if (ManaText[i] != null && unit.GetElementManaPrice(i) != 0)
            {
                ManaText[i].text = $"<color={unit.GetElementColorString(i)}>{unit.GetElementManaPrice(i)}</color>";
            }
            else
            {
                ManaText[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetCardUnit(Unit unit)
    {
        this.unit = unit;
        unitPrefab = unit.gameObject;
    }

    public GameObject GetUnitPrefab()
    {
        return unitPrefab;
    }
}
