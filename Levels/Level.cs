using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Level/New level", fileName = "Level")]
public class Level: ScriptableObject
{
    public int Code;
    [SerializeField]
    public Tilemap Map;
    public List<Enemies> enemies;
    public Coroutine Conductor { get; private set; }

    [System.Serializable]
    public class Enemies
    {
        [SerializeField]
        private UnitBasis Unit;
        [SerializeField]
        public int Count;
    }

    void GetMap()
    {
        //Map1.
    }

}


