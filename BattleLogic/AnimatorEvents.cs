using UnityEngine;

public partial class AnimatorEvents : MonoBehaviour
{
    public void Death()
    {
        gameObject.GetComponentInParent<Stats>().DeathAnimationEnds();
    }
}
