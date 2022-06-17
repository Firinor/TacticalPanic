using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FirGamesTileHelper
{
    [CreateAssetMenu(menuName = "Level/New tile", fileName = "TerraTile")]
    public class TerraTile : TileBase
    {
        public int value;
        public Zone mapSector { get; set; }
        public Zone mapZone { get; set; }
        public bool stuffed { get; set; }

        public TerraTile(int x, int y, int v = 0)
        {
            this.value = v;
        }
    }

    public class Zone
    {
        public bool stuffed;
        public List<TerraTile> tiles;
    }
}