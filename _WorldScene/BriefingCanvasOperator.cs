using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCanvasOperator : SinglBehaviour<BriefingCanvasOperator>
{
    [SerializeField]
    private GameObject partyPanel;
    [SerializeField]
    private GameObject enemyPanel;
    [SerializeField]
    private BriefingMapOperator briefingMapOperator;

    private Level level;

    internal static Transform GetPartyTransform()
    {
        return instance.partyPanel.transform;
    }

    internal static void SetLevelInfo(Level level)
    {
        if(instance.level != level)
        {
            instance.briefingMapOperator.SetLevelInfo(level);
            UnitsCardManager.CardsToParent(level.GetEnemies(), instance.enemyPanel.transform);
        }
    }

    public void LoadLevelInfo(int levelCode)
    {
        //tileMapOperator.GenerateMap();
    }
}
