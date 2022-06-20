using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//public enum TileTipe { Grass, EnemySpawner, ProtectedPoint, Road, Wood, Rock, River }

[CreateAssetMenu(menuName = "Level/New level", fileName = "Level")]
public class Level: ScriptableObject
{
    public int Code;
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    [Multiline]
    private string descriptionText;
    [SerializeField]
    public Tilemap Map;
    private List<List<int>> intMap;
    public List<Enemies> enemies;
    public Coroutine Conductor { get; private set; }
    public string DescriptionText { get { return descriptionText; } }

    internal List<UnitBasis> GetEnemies()
    {
        List<UnitBasis> result = new List<UnitBasis>();
        foreach (Enemies enemy in enemies)
        {
            result.Add(enemy.UnitBasis);
        }
        return result;
    }

    [System.Serializable]
    public class Enemies
    {
        [SerializeField]
        private UnitInformator Unit;
        [SerializeField]
        public int Count;

        public UnitBasis UnitBasis
        {
            get
            {
                return Unit.unitBasis;
            }
        }
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
                    intMap[x].Add(WorldMapInformator.GetTileIndex(levelTile));
                }
            }
        }

        return intMap;
    }

}


