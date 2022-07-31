using System;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public enum TileTipe { Grass, EnemySpawner, ProtectedPoint, Road, Wood, Rock, River }

    public static class TileMapGenerator// Top-manager
    {
        public static List<List<TerraTile>> mapCode;
        private static int width;
        private static int height;
        private static Vector2Int[] playerPoint;
        private static Vector2Int[] enemies;
        private static Vector2Int[] roadPoints;
        private static List<TerraTile> unoccupiedTiles;
        private static System.Random random = new System.Random((int)Time.timeSinceLevelLoad);
        /*
         * all map sectors
         * 
         *  0  1  2  3
         *  4  5  6  7
         *  8  9 10 11
         * 12 13 14 15
         */
        private static int divider = 4;
        private static Zone[] mapSectors = new Zone[divider * divider];
        /*
        * all map zones
        * 1 1 2 2
        * 1 0 0 2
        * 3 0 0 4
        * 3 3 4 4
        */
        private static Zone[] mapZones = new Zone[5];

        public static List<List<TerraTile>> GenerateNewMap(int x, int y)
        {
            width = x;
            height = y;

            mapCode = NewListListTile();
            unoccupiedTiles = GetAllTiles();
            mapSectors = DivideTheMapIntoSectors();
            mapZones = DistributeTheSectorsIntoZones(mapSectors);

            GenerateProtectedPoint();
            GenerateEnemySpawners();
            GenerateRoadPoints();
            GenerateRocksAndRivers();
            //GenerateConnectedRoads(mapCode, tileMap);

            //for (int i = 0; i < mapCode.GetLength(0); i++)
            //    Debug.Log(string.Join(",", mapCode[i]));

            return mapCode;
        }
        static List<List<TerraTile>> NewListListTile()
        {
            var result = new List<List<TerraTile>>();
            for (int _x = 0; _x < width; _x++)
            {
                result.Add(new List<TerraTile>());
                result[_x] = new List<TerraTile>();
                for (int _y = 0; _y < height; _y++)
                {
                    result[_x].Add(new TerraTile(_x, _y));
                }
            }

            return result;
        }
        private static Zone[] DivideTheMapIntoSectors()
        {
            Zone[] zones = new Zone[divider * divider];

            for (int i = 0; i < zones.Length; i++)
            {
                zones[i] = new Zone();
                zones[i].tiles = new List<TerraTile>();
            }

            foreach (TerraTile tile in unoccupiedTiles)
            {
                //int dx = Mathf.FloorToInt(((float)tile.x / (float)width) * divider);
                //int dy = Mathf.FloorToInt(((float)tile.y / (float)height) * divider);
                //int i = dx + dy * divider;
                //zones[i].tiles.Add(tile);//for divider == 4 ; from 0 to 15
                //tile.mapSector = zones[i];
                //if ((i + (dy % 2 == 0 ? 0 : 1)) % 2 == 0)
                //{
                //    tile.value = 4;
                //}
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

            TilesForZone(zones[0], new int[] { 5, 6, 9, 10 });
            TilesForZone(zones[1], new int[] { 0, 1, 4 });
            TilesForZone(zones[2], new int[] { 2, 3, 7 });
            TilesForZone(zones[3], new int[] { 8, 12, 13 });
            TilesForZone(zones[4], new int[] { 11, 14, 15 });

            void TilesForZone(Zone zone, int[] ints)
            {
                zone = new Zone();
                zone.tiles = new List<TerraTile>();
                foreach (int i in ints)
                {
                    CopyTiles(zone, mapSectors[i]);
                }
            }

            void CopyTiles(Zone takerZone, Zone giverZone)
            {
                foreach (TerraTile tile in giverZone.tiles)
                {
                    takerZone.tiles.Add(tile);
                    tile.mapZone = takerZone;
                }
            }

            return zones;
        }
        private static List<TerraTile> GetAllTiles()
        {
            List<TerraTile> tiles = new List<TerraTile>();

            for (int _x = 0; _x < width; _x++)
            {
                for (int _y = 0; _y < height; _y++)
                {
                    tiles.Add(mapCode[_x][_y]);
                }
            }

            return tiles;
        }
        private static void GenerateProtectedPoint()
        {
            playerPoint = new Vector2Int[1];

            playerPoint[0].x = random.Next(width);
            playerPoint[0].y = random.Next(height);

            mapCode[playerPoint[0].x][playerPoint[0].y].value = 1;

            TileMath.BookATerritory(playerPoint[0], 5, unoccupiedTiles, stufferStatus: true);
        }
        private static void GenerateEnemySpawners()
        {
            enemies = new Vector2Int[1];

            List<TerraTile> tiles = TileMath.GetAllowedTiles(mapCode);

            TerraTile chosenTile = tiles[random.Next(tiles.Count)];

            chosenTile.value = 2;

            TileMath.BookATerritory(chosenTile, 3, unoccupiedTiles, stufferStatus: true);
        }
        private static void GenerateRoadPoints()
        {
            roadPoints = new Vector2Int[3];

            for (int i = 0; i < 3; i++)
            {
                roadPoints[i].x = random.Next(width);
                roadPoints[i].y = random.Next(height);
                mapCode[roadPoints[i].x][roadPoints[i].y].value = 3;
            }
        }
        private static void GenerateRocksAndRivers()
        {
            TileMath.DrawLine(roadPoints[0], roadPoints[1], mapCode);
        }
        private static void GenerateConnectedRoads()
        {

        }

        internal static List<List<TerraTile>> LoadLevel(int level)
        {
            switch (level)
            {
                case 1: return Level1();
                default: return new List<List<TerraTile>>();
            }
        }

        private static List<List<TerraTile>> Level1()
        {
            List<List<TerraTile>> level = new List<List<TerraTile>>();

            int[,] map = new int[20, 30]
            {
            { 6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6 },
            { 6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 4,4,4,0,0,0,4,4,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,5,5,5,5,5 },
            { 4,4,4,4,4,0,4,4,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,5,5,5,5,5,5 },
            { 4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,0 },
            { 4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,2 },
            { 4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            };

            for (int y = 0; y < 20; y++)
            {
                level.Add(new List<TerraTile>());
                for (int x = 0; x < 30; x++)
                {
                    level[y].Add(new TerraTile(x, y));
                    level[y][x].value = map[y, x];
                }
            }
            return level;
        }
    }
}

//public class TileMap
//{
//    public int x{get; private set;}
//    public int y{get; private set;}
//    public Tile[,] tiles { get; private set; }

//    public TileMap(Vector2 vector) : this((int)vector.x, (int)vector.y) { }
//    public TileMap(Vector2Int vector) : this(vector.x, vector.y){ }
//    public TileMap(int x, int y)
//    {
//        this.x = x;
//        this.y = y;
//        tiles = new Tile[x,y];

//        for (int _x = 0; _x < x; _x++)
//            for(int _y = 0; _y < y; _y++)
//            {
//                //tiles[_x,_y] = new Tile();
//            }
        
//    }

    
//}
