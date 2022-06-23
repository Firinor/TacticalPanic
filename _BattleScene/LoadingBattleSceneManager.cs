using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBattleSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHand;
    [SerializeField]
    private GameObject playerCardPrefab;
    [SerializeField]
    private GameObject flor;
    private List<GameObject> battleTiles = new List<GameObject>();

    void Awake()
    {
        SelectedUnitsInformator.Start();
        UnitInfoPanelOperator.InfoEvent += UnitInfoPanelOperator.RefreshPointsInfo;
        CreateCards();
        CreatrBattleField();
    }

    private void CreatrBattleField()
    {
        Level level = PlayerManager.PickedLevel;
        GenerateMiniMap(level.GetMap());
    }

    private void GenerateMiniMap(List<List<int>> intMap)
    {
        if (battleTiles.Count > 0)
        {
            foreach (GameObject sprite in battleTiles)
            {
                Destroy(sprite);
            }
            battleTiles.Clear();
        }

        for (int x = 0; x < intMap.Count; x++)
        {
            for (int y = 0; y < intMap[x].Count; y++)
            {
                GameObject newTile = Instantiate(TileInformator.BattleTiles[intMap[x][y]], flor.transform);
                battleTiles.Add(newTile);
                newTile.transform.SetPositionAndRotation(new Vector3(x,0,y), Quaternion.identity);
            }
        }
    }

    private void CreateCards()
    {
        for (int i = 0; i < PlayerManager.Party.Count; i++)
        {
            GameObject Card = Instantiate(playerCardPrefab, playerHand.transform);
            CardStats cardStats = Card.GetComponent<CardStats>();
            cardStats.SetCardUnit(PlayerManager.Party[i]);
        }
    }
}
