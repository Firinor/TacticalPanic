using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTavernCardOperator : MonoBehaviour
{
    [SerializeField]
    private UnitBasis unit;
    private UnitSprite unitSprite;
    [SerializeField]
    private Image image;

    public void SetUnit(UnitBasis unit)
    {
        this.unit = unit;
        unitSprite = unit.GetUnitSprite();
        image.sprite = unitSprite.unitFace;
    }
}
