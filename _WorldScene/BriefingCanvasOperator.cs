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

    private int levelToLoad;

    internal static Transform GetPartyTransform()
    {
        return instance.partyPanel.transform;
    }

    internal static void SetLevelInfo(Level level)
    {
        instance.briefingMapOperator.SetLevelInfo(level);
    }

    public void LoadLevelInfo(int levelCode)
    {
        //tileMapOperator.GenerateMap();
    }
}
