using System;
using System.Collections.Generic;
using UnityEngine;

namespace FirGamesTileHelper
{
    public static class TileMath
    {
        public static void DrawALine(Tile a, Tile b, List<List<Tile>> map, TileTipe tipe = TileTipe.Road)
        {
            
        }

        public static void BookATerritory(Tile a, int radius, List<List<Tile>> map, TileTipe tipe = TileTipe.Wood)
        {

        }

        public static void BookATerritory(Tile a, int radius, List<List<Tile>> map, bool stufferStatus = true)
        {

        }

        public static void BookATerritory(Tile tile, int radius, List<Tile> tiles, bool stufferStatus = true)
        {
            BookATerritory(new Vector2Int(tile.x, tile.y), radius, tiles, stufferStatus);
        }

        public static void BookATerritory(Vector2Int point, int radius, List<Tile> tiles, bool stufferStatus = true)
        {
            List<Tile> tempTileList = new List<Tile>();

            foreach (Tile tile in tiles)
            {
                if(tile == null)
                    continue;
                if(tile.x <= point.x + radius && tile.x >= point.x - radius
                    && tile.y <= point.y + radius && tile.y >= point.y - radius)
                {
                    tile.value = 4;
                    tile.stuffed = true;
                    tempTileList.Add(tile);
                }
                if(tile.x == point.x && tile.y == point.y)
                {
                    tile.mapSector.stuffed = stufferStatus;
                    tile.mapZone.stuffed = stufferStatus;
                    tile.value = 1;
                }
            }

            foreach (Tile tile in tempTileList)
            {
                tiles.Remove(tile);
            }
        }

        internal static List<Tile> GetAllowedTiles(List<List<Tile>> mapCode)
        {
            List<Tile> tiles = new List<Tile>();

            foreach (List<Tile> listTile in mapCode)
            {
                foreach (Tile tile in listTile)
                {
                    if(tile.mapZone != null && !tile.mapZone.stuffed && !tile.stuffed)
                    {
                        tiles.Add(tile);
                    }
                }
            }

            return tiles;
        }
    }
}