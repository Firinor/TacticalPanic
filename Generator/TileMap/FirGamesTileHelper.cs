using System;
using System.Collections.Generic;
using UnityEngine;

namespace FirGamesTileHelper
{
    public static class TileMath
    {
        //public static void DrawALine(Tile a, Tile b, List<List<Tile>> map, TileTipe tipe = TileTipe.Road)
        public static void DrawALine(Graphics g, int x0, int y0, int x1, int y1, int blockSize)
        {
            {
                int scaledX0 = x0 / blockSize;
                int scaledY0 = y0 / blockSize;
                int scaledX1 = x1 / blockSize;
                int scaledY1 = y1 / blockSize;
                int dx = scaledX1 - scaledX0;
                int dy = scaledY1 - scaledY0;
                int stepX = 0; //Integer.signum(dx);
                int stepY = 0; //Integer.signum(dy);
                //dx = Math.abs(dx);
                //dy = Math.abs(dy);
                int dx2 = dx << 1;
                int dy2 = dy << 1;
                int x = scaledX0;
                int y = scaledY0;
                int error;
                if (dx >= dy)
                {
                    error = dy2 - dx;
                    do
                    {
                        plot(g, x, y, blockSize);
                        if (error > 0)
                        {
                            y += stepY;
                            error -= dx2;
                        }
                        error += dy2;
                        x += stepX;
                    } while (x != scaledX1);
                }
                else
                {
                    error = dx2 - dy;
                    do
                    {
                        plot(g, x, y, blockSize);
                        if (error > 0)
                        {
                            x += stepX;
                            error -= dy2;
                        }
                        error += dx2;
                        y += stepY;
                    } while (y != scaledY1);
                }
            }

            static void plot(Graphics g, int x, int y, int blockSize)
            {
                int x0 = x * blockSize;
                int y0 = y * blockSize;
                int w = blockSize;
                int h = blockSize;
                //g.fillRect(x0, y0, w, h);
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