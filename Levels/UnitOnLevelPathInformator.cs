using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/New path", fileName = "Path")]
public class UnitOnLevelPathInformator : ScriptableObject
{
    public LevelInformator level;
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

    internal Vector3 GetSpawnPoint()
    {
        Vector2Int point = level.EnemySpawnPoints[start];
        return new Vector3(point.x, 0.5f, point.y);
    }
}
