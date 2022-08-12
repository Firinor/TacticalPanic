using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TacticalPanicCode
{
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
            //UnitInfoPanelOperator.InfoEvent += UnitInfoPanelOperator.RefreshPointsInfo;
            CreatePlayerUnits();
            CreateEnemyUnits();
            CreateBattleField();
        }

        private void CreateEnemyUnits()
        {
            foreach (EnemySquadsInformator squad in PlayerManager.PickedLevel.enemies)
            {
                squad.CleareUnitList();
                LastLoadingUnit = squad.UnitBasis;
                for (int i = 0; i < squad.Count; i++)
                {
                    GameObject unitGameObject = Instantiate(unitPrefab, UnitsManager.EnemyUnitsParent.transform);
                    unitGameObject.name = unitGameObject.name + $" - {i}";
                    UnitOperator unit = unitGameObject.GetComponent<UnitOperator>();
                    unit.Prepare(ConflictSide.Enemy);
                    EnemyManager.AddEnemyToDictionary(unit, squad);
                }
            }
        }

        [ContextMenu("CreateBattleField")]
        private void CreateBattleFieldByContextMenu()
        {
            TileInformator tileInformator = GameObject.Find("BigDataBase").GetComponent<TileInformator>();
            tileInformator.SingletoneCheck(tileInformator);
            CreateBattleField(GameObject.Find("LoadDebuger").GetComponent<LoadDebuger>().level);
        }
        private void CreateBattleField()
        {
            CreateBattleField(PlayerManager.PickedLevel);
        }
        private void CreateBattleField(LevelInformator level)
        {
            List<List<int>> intMap = level.GetMap();
            ClearBattleField();

            for (int x = 0; x < intMap.Count; x++)
            {
                for (int y = 0; y < intMap[x].Count; y++)
                {
                    GameObject newTile = Instantiate(TileInformator.BattleTiles[intMap[x][y]], flor.transform);
                    battleTiles.Add(newTile);
                    newTile.transform.SetPositionAndRotation(new Vector3(x, 0, y), Quaternion.identity);
#if UNITY_EDITOR
                    newTile.GetComponent<inBattleTileOperator>().text = $"{x}-{y}";
#else
                newTile.GetComponent<inBattleTileOperator>().Switch.SetActive(false);
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

        private void ClearBattleField()
        {
            if (battleTiles.Count > 0)
            {
                foreach (GameObject tile in battleTiles)
                {
                    Destroy(tile);
                }
                battleTiles.Clear();
            }

            if(flor.transform.childCount > 0)
            {
                foreach (inBattleTileOperator tile in flor.transform.GetComponentsInChildren<inBattleTileOperator>())
                {
                    Destroy(tile.gameObject);
                }
            }
        }

        private void CreatePlayerUnits()
        {
            List<UnitOperator> playerParty = new List<UnitOperator>();
            for (int i = 0; i < PlayerManager.Party.Count; i++)
            {
                LastLoadingUnit = PlayerManager.Party[i];
                playerParty.Add(Instantiate(unitPrefab, UnitsManager.PlayerUnitsParent.transform).GetComponent<UnitOperator>());
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
                UnitCardStats cardStats = Card.GetComponent<UnitCardStats>();
                cardStats.SetCardUnit(playerParty[i]);
            }
        }
    }
}
