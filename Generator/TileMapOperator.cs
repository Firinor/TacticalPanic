using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapOperator : MonoBehaviour
{
    [SerializeField]
    private GameObject tileGroup;
    [SerializeField]
    private GameObject[] tilePrefabs;
    [SerializeField]
    private int tileSize;
    [SerializeField]
    private Vector2Int mapSize;

    public void GenerateNewMap()
    {
        TileMap tileMap = TileMapGenerator.GenerateNewMap(mapSize.x, mapSize.y);
        float offset = tileSize / 2;

        for (int x = 0; x < tileMap.x; x++)
            for(int y = 0; y < tileMap.y; y++)
            {
                GameObject tile = Instantiate(TileByInt(tileMap.tiles[x, y].tileClass), tileGroup.transform);
                tile.transform.localPosition = new Vector3(offset + x * tileSize, offset + y * tileSize, 0);
            }
    }

    private GameObject TileByInt(TileClass tileClass)
    {
        return tilePrefabs[(int)tileClass];
    }
}
