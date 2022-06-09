using System;
using UnityEngine;

public enum TileClass { EnemySpawner, ProtectedPoint, Road, Grass, Wood, Rock, River }

public static class TileMapGenerator// Top-manager
{
    public static int[][] mapCode;
    private static int width;
    private static int height;

    private static Vector2Int[] playerPoint;
    private static Vector2Int[] enemies;
    private static Vector2Int[] roadPoints;


    private static System.Random random = new System.Random();

    private class Zone
    {
        public bool stuffed;
    }

    /*
     * all map zones
     * 
     * 1 1 2 2
     * 1 1 2 2
     * 3 3 4 4
     * 3 3 4 4
     */
    private static Zone[] mapZones = new Zone[4];

    public static TileMap GenerateNewMap(int x, int y) 
    {
        width = x;
        height = y;

        TileMap tileMap = new TileMap(x, y);
        mapCode = new int[x][];
        for(int i = 0; i < x; i++)
        {
            mapCode[i] = new int[y];
        }

        GenerateProtectedPoint(mapCode, tileMap);
        GenerateEnemySpawners(mapCode, tileMap);
        GenerateRoadPoints(mapCode, tileMap);
        GenerateRocksAndRivers(mapCode, tileMap);
        GenerateConnectedRoads(mapCode, tileMap);

        //for (int i = 0; i < mapCode.GetLength(0); i++)
        //    Debug.Log(string.Join(",", mapCode[i]));

        return tileMap;
    }

    private static void GenerateProtectedPoint(int[][] mapCode, TileMap tileMap)
    {
        playerPoint = new Vector2Int[1];

        playerPoint[0].x = random.Next(width);
        playerPoint[0].y = random.Next(height);

        mapCode[playerPoint[0].x][playerPoint[0].y] = 1;

        foreach (Vector2Int point in playerPoint)
        {
            tileMap.tiles[point.x, point.y].tileClass = TileClass.ProtectedPoint;
        }
    }

    private static void GenerateEnemySpawners(int[][] mapCode, TileMap tileMap)
    {
        enemies = new Vector2Int[1];

        enemies[0].x = random.Next(width);
        enemies[0].y = random.Next(height);

        mapCode[enemies[0].x][enemies[0].y] = 0;

        foreach (Vector2Int enemy in enemies)
        {
            tileMap.tiles[enemy.x, enemy.y].tileClass = TileClass.EnemySpawner;
        }
    }

    private static void GenerateRoadPoints(int[][] mapCode, TileMap tileMap)
    {
        roadPoints = new Vector2Int[3];
        
        for(int i = 0; i < 3; i++)
        {
            roadPoints[i].x = random.Next(width);
            roadPoints[i].y = random.Next(height);
            mapCode[roadPoints[i].x][roadPoints[i].y] = 2;
        }

        foreach (Vector2Int road in roadPoints)
        {
            tileMap.tiles[road.x, road.y].tileClass = TileClass.Road;
        }
    }

    private static void GenerateRocksAndRivers(int[][] mapCode, TileMap tileMap)
    {
        
    }

    private static void GenerateConnectedRoads(int[][] mapCode, TileMap tileMap)
    {
        
    }
}

public class TileMap
{
    public int x{get; private set;}
    public int y{get; private set;}
    public Tile[,] tiles { get; private set; }

    public TileMap(Vector2 vector) : this((int)vector.x, (int)vector.y) { }
    public TileMap(Vector2Int vector) : this(vector.x, vector.y){ }
    public TileMap(int x, int y)
    {
        this.x = x;
        this.y = y;
        tiles = new Tile[x,y];

        for (int _x = 0; _x < x; _x++)
            for(int _y = 0; _y < y; _y++)
            {
                tiles[_x,_y] = new Tile();
            }
        
    }

    
}
