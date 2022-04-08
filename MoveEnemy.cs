using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Stats _stats;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void Deactivate()
    {
        enabled = false;
    }

    void OnMouseOver()
    {
        _stats.Damage(SceneStats.HPDestroyPower, Stats.Points.HP);
    }
}
