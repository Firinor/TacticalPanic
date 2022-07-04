using UnityEngine;

public class PlayerDebuger : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform skinRootTransform;
    private Vector3 moveInput;

    [SerializeField]
    private UnitOperator stats;

    public void Start()
    {
        
    }

    public void FixedUpdate()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if (ver == 0 && hor == 0)
            return;

        moveInput = new Vector3(transform.position.x + hor, transform.position.y + ver, transform.position.y + ver);
        if (hor > 0 && skinRootTransform.localScale.x<0)
        {
            skinRootTransform.localScale = new Vector3(1,1,1);
        }
        else if (hor < 0 && skinRootTransform.localScale.x > 0)
        {
            skinRootTransform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = Vector3.MoveTowards(transform.position, moveInput, speed * Time.fixedDeltaTime);
    }

    public void Deactivate()
    {
        enabled = false;
    }
}
