using System;
using System.Collections;
using UnityEngine;

public class MoveOperator : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private Vector3 target;
    private bool moveOn;

    [SerializeField]
    private UnitOperator unitOperator;
    private UnitOnLevelPathInformator path;

    public void Update()
    {
        float deltaTime = Time.deltaTime;
        if (moveOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * deltaTime);
            if (UnitOnTarget())
            {
                moveOn = false;
            }
        }
    }

    private bool UnitOnTarget()
    {
        return (int)transform.position.x == (int)target.x
            && (int)transform.position.z == (int)target.z;//y
    }

    public void Deactivate()
    {
        enabled = false;
    }

    internal void SpawnToPoint(Vector3 point)
    {
        gameObject.transform.position = point;
    }

    internal IEnumerator FollowThPath(UnitOnLevelPathInformator enemyPath)
    {
        path = enemyPath;
        SpawnToPoint(path.GetSpawnPoint());
        yield return new WaitForSeconds(0.5f);
        if(path.points.Length > 0)
        {
            target = path.GetPoint(0);
            moveOn = true;
            for (int i = 1; i < path.points.Length; i++)
            {
                yield return !moveOn;
                target = path.GetPoint(i);
                moveOn = true;
            }
        }
        target = path.GetExitPoint();
        moveOn = true;
        StopCoroutine("FollowThPath");
    }
}
