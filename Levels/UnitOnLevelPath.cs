using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/New path", fileName = "Path")]
public class UnitOnLevelPath : ScriptableObject
{
    public Level level;
    [Range(0, 5)]
    public int start;
    [Range(0, 5)]
    public int finish;
    public wayPoint[] points;

    [System.Serializable]
    public class wayPoint
    {
        public Vector2Int Point;
        public float delay;
    }
}
