using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirGamesTileHelper;

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

    private List<Transform> transforms = new List<Transform>();

    public void GenerateNewMap()
    {
        if(transforms.Count > 0)
        {
            foreach (Transform tr in transforms)
            {
                Destroy(tr.gameObject);
            }
            transforms.Clear();
        }

        List<List<Tile>> intMap = TileMapGenerator.GenerateNewMap(mapSize.x, mapSize.y);
        float offset = tileSize / 2;

        for (int x = 0; x < mapSize.x; x++)
            for(int y = 0; y < mapSize.y; y++)
            {
                GameObject tile = Instantiate(TileByInt(intMap[x][y].value), tileGroup.transform);
                tile.transform.localPosition = new Vector3(offset + x * tileSize, offset + y * tileSize, 0);
                transforms.Add(tile.transform);
            }
    }

    private GameObject TileByInt(int i)
    {
        return tilePrefabs[i];
    }
}
