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
    private GameObject unitPrefab;
    [SerializeField]
    private GameObject flor;
    private List<GameObject> battleTiles = new List<GameObject>();

    public static UnitBasis LastLoadingUnit { get; internal set; }

    void Awake()
    {
        SelectedUnitsInformator.Start();
        UnitInfoPanelOperator.InfoEvent += UnitInfoPanelOperator.RefreshPointsInfo;
        CreatePlayerUnits();
        CreateBattleField();
    }

    private void CreateBattleField()
    {
        Level level = PlayerManager.PickedLevel;
        List<List<int>> intMap = level.GetMap();
    
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
                #if UNITY_EDITOR
                newTile.GetComponent<inBattleTileOperator>().text.text = $"{x}-{y}";
                #endif
            }
        }

        foreach (Vector2Int point in level.EnemySpawnPoints)
        {
            GameObject newTile = Instantiate(TileInformator.BattleTiles[2], flor.transform);
            battleTiles.Add(newTile);
            newTile.transform.SetPositionAndRotation(new Vector3(point.x, 0, point.y), Quaternion.identity);
        }
        foreach (Vector2Int point in level.PlayerHealthPoints)
        {
            GameObject newTile = Instantiate(TileInformator.BattleTiles[1], flor.transform);
            battleTiles.Add(newTile);
            newTile.transform.SetPositionAndRotation(new Vector3(point.x, 0, point.y), Quaternion.identity);
        }

    }

    private void CreatePlayerUnits()
    {
        List<Unit> playerParty = new List<Unit>();
        for (int i = 0; i < PlayerManager.Party.Count; i++)
        {
            LastLoadingUnit = PlayerManager.Party[i];
            playerParty.Add(Instantiate(unitPrefab).GetComponent<Unit>());
        }

        Transform handTransform = playerHand.transform;
        List<GameObject> childToDestroy = new List<GameObject>();
        if (handTransform.childCount > 0)
        {
            for (int i = 0; i < handTransform.childCount; i++)
                childToDestroy.Add(handTransform.GetChild(i).gameObject);
        }
        foreach (GameObject child in childToDestroy)
        {
                Destroy(child);
        }

        for (int i = 0; i < playerParty.Count; i++)
        {
            GameObject Card = Instantiate(playerCardPrefab, playerHand.transform);
            CardStats cardStats = Card.GetComponent<CardStats>();
            cardStats.SetCardUnit(playerParty[i]);
        }
    }
}
