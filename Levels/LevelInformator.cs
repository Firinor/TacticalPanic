using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//public enum TileTipe { Grass, EnemySpawner, ProtectedPoint, Road, Wood, Rock, River }

[CreateAssetMenu(menuName = "Level/New level", fileName = "Level")]
public class LevelInformator: ScriptableObject
{
    public int Code;
    private int width = 30;
    private int height = 20;
    [SerializeField]
    [Multiline]
    private string descriptionText;
    [SerializeField]
    public Tilemap Map;
    private List<List<int>> intMap;
    public List<EnemySquadsInformator> enemies;
    [SerializeField]
    private List<Vector2Int> enemySpawnPoints;
    public List<Vector2Int> EnemySpawnPoints { get { return enemySpawnPoints; } }
    [SerializeField]
    private List<Vector2Int> playerHealthPoints;
    public List<Vector2Int> PlayerHealthPoints { get { return playerHealthPoints; } }
    public Coroutine Conductor { get; private set; }
    public string DescriptionText { get { return descriptionText; } }

    internal List<UnitBasis> GetEnemyBases()
    {
        List<UnitBasis> result = new List<UnitBasis>();
        foreach (EnemySquadsInformator enemy in enemies)
        {
            if(!result.Equals(enemy))
                result.Add(enemy.UnitBasis);
        }
        return result;
    }

    public List<List<int>> GetMap()
    {
        if(intMap == null)
        {
            intMap = new List<List<int>>();
            for(int x = 0; x<width; x++)
            {
                intMap.Add(new List<int>());
                for(int y = 0; y<height; y++)
                {
                    Tile levelTile = Map.GetTile<Tile>(new Vector3Int(x, y, 0));
                    intMap[x].Add(TileInformator.GetTileIndex(levelTile));
                }
            }
        }

        return intMap;
    }
}