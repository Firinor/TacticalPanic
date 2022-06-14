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
    [Range(1, 50)]
    public static int level = 1;
    [SerializeField]
    private int tileSize;
    [SerializeField]
    private Vector2Int mapSize;

    private List<Transform> transforms = new List<Transform>();

    public void GenerateMap()
    {
        if (transforms.Count > 0)
        {
            foreach (Transform tr in transforms)
            {
                Destroy(tr.gameObject);
            }
            transforms.Clear();
        }

        //List<List<Tile>> intMap = TileMapGenerator.GenerateNewMap(mapSize.x, mapSize.y);
        List<List<Tile>> intMap = TileMapGenerator.LoadLevel(level);
        Instantiate(intMap);
    }

    private void Instantiate(List<List<Tile>> intMap)
    {
        float offset = tileSize / 2;
        
        for (int y = 0; y < mapSize.y; y++)
            for (int x = 0; x < mapSize.x; x++)
            {
                GameObject tile = Instantiate(TileByInt(intMap[y][x].value), tileGroup.transform);
                tile.transform.localPosition = new Vector3(offset + x * tileSize, offset + y * tileSize, 0);
                transforms.Add(tile.transform);
            }
    }

    private GameObject TileByInt(int i)
    {
        return tilePrefabs[i];
    }
}
