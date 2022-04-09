using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject skinRoot;

    private Transform skinRootTransform;
    private Vector3 moveInput;

    [SerializeField]
    private Stats _stats;

    void Start()
    {
        skinRootTransform = skinRoot.transform;
    }

    void FixedUpdate()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        moveInput = new Vector3(transform.position.x + hor, transform.position.y + ver, transform.position.y + ver);
        if (hor > 0 && skinRootTransform.localScale.x<0)
        {
            skinRootTransform.localScale = new Vector3(1,1,1);
            //SkinRootTransform.position = new Vector3(-SkinRootTransform.position.x,
            //    SkinRootTransform.position.y,
            //    SkinRootTransform.position.z);
        }
        else if (hor < 0 && skinRootTransform.localScale.x > 0)
        {
            skinRootTransform.localScale = new Vector3(-1, 1, 1);
            //SkinRootTransform.position = new Vector3(-SkinRootTransform.position.x,
            //    SkinRootTransform.position.y,
            //    SkinRootTransform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, moveInput, Time.fixedDeltaTime * speed);
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
