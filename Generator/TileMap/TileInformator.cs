using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TacticalPanicCode
{
    public class TileInformator : SinglBehaviour<TileInformator>
    {
        [SerializeField]
        private List<Tile> tiles;
        [SerializeField]
        private List<GameObject> spriteTiles;
        [SerializeField]
        private List<GameObject> inBattleTiles;

        private Dictionary<Tile, GameObject> tileSpritePairs;

        void Awake()
        {
            SingletoneCheck<TileInformator>(this);
            tileSpritePairs = new Dictionary<Tile, GameObject>();
            for (int i = 0; i < tiles.Count; i++)
            {
                tileSpritePairs.Add(tiles[i], spriteTiles[i]);
            }
        }

        public static List<Tile> Tiles { get { return instance.tiles; } }

        public static List<GameObject> SpriteTiles { get { return instance.spriteTiles; } }
        public static List<GameObject> BattleTiles { get { return instance.inBattleTiles; } }

        public static GameObject GetTileAsSprite(Tile tile)
        {
            return instance.tileSpritePairs[tile];
        }

        public static int GetTileIndex(Tile tile)
        {
            return instance.tiles.IndexOf(tile);
        }

        public static GameObject GetInBattleTile(Tile tile)
        {
            return instance.inBattleTiles[GetTileIndex(tile)];
        }
    }
}
