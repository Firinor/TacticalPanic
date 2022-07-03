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

    private LevelInformator level;

    internal static Transform GetPartyTransform()
    {
        return instance.partyPanel.transform;
    }

    internal static void SetLevelInfo(LevelInformator level)
    {
        if(instance.level != level)
        {
            List<UnitBasis> enemies = level.GetEnemyBases();
            instance.briefingMapOperator.SetLevelInfo(level);
            UnitsCardManager.CardsToParent(enemies, instance.enemyPanel.transform);
            UnitsCardManager.DirectionOfCards(enemies, CardDirectionView.Left);
        }
    }
}
