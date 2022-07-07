using System;
using System.Collections;
using UnityEngine;

public class MoveOperator : MonoBehaviour
{
    private float speed = 3f;
    private float turningSpeed = 3;
    private Vector3 target;
    private float directionOfMovement;
    private float DirectionOfMovement
    {
        get
        {
            return directionOfMovement;
        }
        set
        {
            directionOfMovement = value % 360;
            while (directionOfMovement < 0)
            {
                directionOfMovement += 360;
            }
        }
    }
    private bool moveOn;
    private UnitOnLevelPathInformator path;

    public void FixedUpdate()
    {
        DirectionOfMovement = 360;
        if (moveOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
                //,target;
            if (UnitOnTarget())
            {
                moveOn = false;
            }
        }
    }

    private bool UnitOnTarget()
    {
        return (int)(transform.position.x*2 + 0.5f) == (int)(target.x*2)
            && (int)(transform.position.z*2 + 0.5f) == (int)(target.z*2);//y
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
