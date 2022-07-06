using System;
using System.Collections;
using UnityEngine;

public class MoveOperator : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

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
        return (int)(transform.position.x + 0.5f) == (int)target.x
            && (int)(transform.position.z + 0.5f) == (int)target.z;//y
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
        int length = path.points.Length;
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                target = path.GetPoint(i);
                moveOn = true;
                while(moveOn)
                    yield return null;
                yield return new WaitForSeconds(path.points[i].delay);
            }
        }
        target = path.GetExitPoint();
        moveOn = true;
        StopCoroutine("FollowThPath");
    }
}
