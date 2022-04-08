using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public void Death()
    {
        gameObject.GetComponentInParent<Stats>().DestroyGameObject();
    }
}
