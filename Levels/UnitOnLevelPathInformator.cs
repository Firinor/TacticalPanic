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
    public WayPoint[] points;

    [System.Serializable]
    public class WayPoint
    {
        public Vector2Int Point;
        public float delay;
    }

    internal Vector3 GetSpawnPoint()
    {
        Vector2Int point = level.EnemySpawnPoints[start];
        return new Vector3(point.x, 0.5f, point.y);
    }

    internal Vector3 GetPoint(int i)
    {
        Vector3 vector3 = new Vector3();
        if(points.Length > i)
        {
            vector3 = new Vector3(points[i].Point.x, 0.5f, points[i].Point.y);
        }
        return vector3; 
    }

    internal Vector3 GetExitPoint()
    {
        Vector2Int point = level.PlayerHealthPoints[finish];
        return new Vector3(point.x, 0.5f, point.y);
    }


}
