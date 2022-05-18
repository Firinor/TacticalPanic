using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerOperator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private float spawnCooldown = 2f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(enemy, GetSpawnPosition(), enemy.transform.rotation);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    Vector3 GetSpawnPosition()
    {
        
        return new Vector3(0f, Random.Range(-4f, 4f), 0f);
        
    }
}
