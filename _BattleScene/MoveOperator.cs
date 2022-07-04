using System;
using UnityEngine;

public class MoveOperator : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private UnitOperator unitOperator;
    private UnitOnLevelPathInformator path;

    public void Update()
    {
        float deltaTime = Time.deltaTime;
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * deltaTime);
    }

    public void Deactivate()
    {
        enabled = false;
    }

    internal void SpawnToPoint(UnitOnLevelPathInformator enemyPath)
    {
        gameObject.transform.position = enemyPath.GetSpawnPoint();
        unitOperator.Deploy();
    }
}
