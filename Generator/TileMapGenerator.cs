using System;
using System.Collections.Generic;
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

    private static List<Tile> unoccupiedTiles;

    private static System.Random random = new System.Random((int)Time.timeSinceLevelLoad);

    private struct Tile {
        public int x;
        public int y;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    private struct Zone
    {
        public bool stuffed;
        public List<Tile> tiles;
    }

    /*
     * all map sectors
     * 
     *  0  1  2  3
     *  4  5  6  7
     *  8  9 10 11
     * 12 13 14 15
     */
    private static int divider = 4;
    private static Zone[] mapSectors = new Zone[divider*divider];
    /*
     * all map zones
     * 1 1 2 2
     * 1 0 0 2
     * 3 0 0 4
     * 3 3 4 4
     */
    private static Zone[] mapZones = new Zone[5];

    public static int[][] GenerateNewMap(int x, int y) 
    {
        width = x;
        height = y;

        mapCode = new int[x][];
        for(int i = 0; i < x; i++)
        {
            mapCode[i] = new int[y];
        }

        unoccupiedTiles = GetAllTiles(x, y);
        mapSectors = DivideTheMapIntoSectors(unoccupiedTiles);
        mapZones = DistributeTheSectorsIntoZones(mapSectors);

        GenerateProtectedPoint();
        //GenerateEnemySpawners(mapCode, tileMap);
        //GenerateRoadPoints(mapCode, tileMap);
        //GenerateRocksAndRivers(mapCode, tileMap);
        //GenerateConnectedRoads(mapCode, tileMap);

        //for (int i = 0; i < mapCode.GetLength(0); i++)
        //    Debug.Log(string.Join(",", mapCode[i]));

        return mapCode;
    }
    private static Zone[] DivideTheMapIntoSectors(List<Tile> unoccupiedTiles)
    {
        Zone[] zones = new Zone[divider * divider];

        for( int i = 0; i < zones.Length; i++)
        {
            zones[i] = new Zone();
            zones[i].tiles = new List<Tile>();
        }

        foreach(Tile tile in unoccupiedTiles)
        {
            int dx = Mathf.FloorToInt((tile.x / (width)) * divider);
            int dy = Mathf.FloorToInt((tile.y / (height)) * divider);
            zones[dx + dy*divider].tiles.Add(tile);//for divider == 4 ; from 0 to 15
        }

        return zones;
    }
    private static Zone[] DistributeTheSectorsIntoZones(Zone[] mapSectors)
    {
        /* all map zones
         * 1 1 2 2
         * 1 0 0 2
         * 3 0 0 4
         * 3 3 4 4
         * 
         * map sectors
         *  0  1  2  3
         *  4  5  6  7
         *  8  9 10 11
         * 12 13 14 15*/

        Zone[] zones = new Zone[5];

        TilesForZone(zones[0], new int[] { 5, 6, 9, 10});
        TilesForZone(zones[1], new int[] { 0, 1, 4 });
        TilesForZone(zones[2], new int[] { 2, 3, 7 });
        TilesForZone(zones[3], new int[] { 8, 12, 13 });
        TilesForZone(zones[4], new int[] { 11, 14, 15 });

        void TilesForZone(Zone zone, int[] ints)
        {
            zone = new Zone();
            zone.tiles = new List<Tile>();
            foreach(int i in ints)
            {
                CopyTiles(zone, mapSectors[i]);
            }
        }

        void CopyTiles(Zone takerZone, Zone giverZone) 
        {
            foreach (Tile tile in giverZone.tiles)
            {
                takerZone.tiles.Add(tile);
            }
        }

        return zones;
    }
    private static List<Tile> GetAllTiles(int x, int y)
    {
        List<Tile> tiles = new List<Tile>();

        for(int _x = 0; _x < x; _x++)
        {
            for (int _y = 0; _y < y; _y++)
            {
                tiles.Add(new Tile(_x, _y));
            }
        }

        return tiles;
    }
    private static void GenerateProtectedPoint()
    {
        playerPoint = new Vector2Int[1];

        playerPoint[0].x = random.Next(width);
        playerPoint[0].y = random.Next(height);

        mapCode[playerPoint[0].x][playerPoint[0].y] = 1;

        Tile tileToRemove = unoccupiedTiles.Find(t => t.x == playerPoint[0].x && t.y == playerPoint[0].y);
        unoccupiedTiles.Remove(tileToRemove);

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
