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
        if(tileGroup.transform.childCount > 0)
        {
            Transform[] Childrens = tileGroup.transform.GetComponentsInChildren<Transform>();
            foreach (Transform Children in Childrens)
            {
                Destroy(Children.gameObject);
            }
        }

        int[][] intMap = TileMapGenerator.GenerateNewMap(mapSize.x, mapSize.y);
        float offset = tileSize / 2;

        for (int x = 0; x < mapSize.x; x++)
            for(int y = 0; y < mapSize.y; y++)
            {
                GameObject tile = Instantiate(TileByInt(intMap[x][y]), tileGroup.transform);
                tile.transform.localPosition = new Vector3(offset + x * tileSize, offset + y * tileSize, 0);
            }
    }

    private GameObject TileByInt(int i)
    {
        return tilePrefabs[i];
    }
}
