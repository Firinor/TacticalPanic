using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour//Top-manager
{
    [SerializeField]
    private List<UnitSprite> unitSprites;

    public static List<UnitBasis> Units { get; private set; }

    void Awake()
    {
        Units = CSVOperator.GetUnits();

        foreach (UnitBasis unit in Units)
        {
            unit.SetUnitSprite(GetUnitSpriteByName(unit.unitName));
        }
    }

    private UnitSprite GetUnitSpriteByName(string name)
    {
        return unitSprites.Find(x => x.Name == name);
    }
}
