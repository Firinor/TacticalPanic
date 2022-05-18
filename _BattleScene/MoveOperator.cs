using UnityEngine;

public class MoveOperator : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Unit stats;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
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
}
