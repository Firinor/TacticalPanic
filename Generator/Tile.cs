using UnityEngine;

public class Tile
{
    public TileClass tileClass{get; set;}
    private GameObject prefab;

    public Tile()
    {
        tileClass = TileClass.Grass;
    }
}