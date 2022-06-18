using FirGamesTileHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DB : SinglBehaviour<DB>//Top-manager
{
    [SerializeField]
    private List<UnitSprite> unitSprites;

    public static List<UnitBasis> Units { get; private set; }

    [SerializeField]
    public List<Level> Levels;

    void Awake()
    {
        SingletoneCheck(this);

        Units = CSVOperator.GetUnits();

        foreach (UnitBasis unit in Units)
        {
            unit.SetUnitSprite(GetUnitSpriteByName(unit.unitName));
        }

        //Levels[0].Map.GetTile( new Vector3Int(1,1,0));
    }

    private UnitSprite GetUnitSpriteByName(string name)
    {
        return unitSprites.Find(x => x.Name == name);
    }

    public static Level ReadLevel(int level)
    {
        //1 - first level
        //2 - second level
        return instance.Levels[level-1];
    }
}
