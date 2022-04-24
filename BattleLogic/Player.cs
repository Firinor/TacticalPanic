using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform skinRootTransform;
    private Vector3 moveInput;

    [SerializeField]
    private Stats _stats;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime * BattleTimer.gameSpeed;
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

        transform.position = Vector3.MoveTowards(transform.position, moveInput, speed * deltaTime);
    }

    //void OnMouseOver()
    //{
    //    _stats.Damage(SceneStats.HPHealPower, Stats.Points.HP);
    //    _stats.Damage(SceneStats.CPHealPower, Stats.Points.CP);
    //}

    public void Deactivate()
    {
        enabled = false;
    }
}
