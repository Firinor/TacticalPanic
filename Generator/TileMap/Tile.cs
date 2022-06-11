using System.Collections.Generic;
using UnityEngine;

namespace FirGamesTileHelper
{
    public class Tile
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public int value { get; set; }
        public Zone mapSector { get; set; }
        public Zone mapZone { get; set; }
        public bool stuffed { get; set; }

        public Tile(int x, int y, int v = 0)
        {
            this.x = x;
            this.y = y;
            this.value = v;
        }
    }

    public class Zone
    {
        public bool stuffed;
        public List<Tile> tiles;
    }
}