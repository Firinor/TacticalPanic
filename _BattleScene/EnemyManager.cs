using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour// SinglBehaviour<EnemyManager>
{
    public static Dictionary<UnitBasis, GameObject> enemies { get; private set; }

    public static void CreateEnemies()
    {
        List<EnemySquadsInformator> enemies = PlayerManager.PickedLevel.enemies;

    }

    Vector3 GetSpawnPosition(int i)
    {
        return new Vector3(0f, Random.Range(-4f, 4f), 0f);
        
    }
}
