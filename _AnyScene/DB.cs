using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour//Top-manager
{
    private static DB inctance;

    [SerializeField]
    private List<UnitSprite> unitSprites;

    public static List<UnitBasis> Units { get; private set; }

    void Awake()
    {
        Singltone();

        Units = CSVOperator.GetUnits();

        foreach (UnitBasis unit in Units)
        {
            unit.SetUnitSprite(GetUnitSpriteByName(unit.unitName));
        }
    }

    private void Singltone()
    {
        if (inctance != null)
            Destroy(gameObject);
        inctance = this;
    }

    private UnitSprite GetUnitSpriteByName(string name)
    {
        return unitSprites.Find(x => x.Name == name);
    }
}
