using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCanvasOperator : SinglBehaviour<BriefingCanvasOperator>
{
    [SerializeField]
    private GameObject partyPanel;
    [SerializeField]
    private TileMapOperator tileMapOperator;

    private int levelToLoad;

    internal static Transform GetPartyTransform()
    {
        return instance.partyPanel.transform;
    }

    public void LoadLevelInfo(int levelCode)
    {
        tileMapOperator.GenerateMap();
    }
}
