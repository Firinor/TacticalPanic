using System;
using System.Collections.Generic;
using UnityEngine;

namespace FirGamesTileHelper
{
    public static class TileMath
    {
        //public static void DrawLine(Tile a, Tile b, List<List<Tile>> map, )
        public static void DrawLine(Vector2Int p0, Vector2Int p1, List<List<Tile>> map, TileTipe tipe = TileTipe.Road)
        {
            DrawLine(p0.x, p0.y, p1.x, p1.y, map, tipe);
        }

        public static void DrawLine(int x0, int y0, int x1, int y1, List<List<Tile>> map, TileTipe tipe = TileTipe.Road)
        {
            {
                int dx = x1 - x0;
                int dy = y1 - y0;
                int stepX = (int)Mathf.Sign(dx);
                int stepY = (int)Mathf.Sign(dy);
                dx = Math.Abs(dx);
                dy = Math.Abs(dy);
                int shift;
                if (dx >= dy)
                {
                    shift = (int)(dx / 2) + dy;
                    do
                    {
                        shift -= dy;
                        map[x0][y0].value = (int)tipe;
                        if (shift < 0)
                        {
                            y0 += stepY;
                            shift += dx;
                        }
                        x0 += stepX;
                    } while (x0 != x1);
                }
                else
                {
                    shift = (int)(dy / 2) + dx;
                    do
                    {
                        shift -= dx;
                        map[x0][y0].value = (int)tipe;
                        if (shift < 0)
                        {
                            x0 += stepX;
                            shift += dy;
                        }
                        y0 += stepY;
                    } while (y0 != y1);
                }
            }
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