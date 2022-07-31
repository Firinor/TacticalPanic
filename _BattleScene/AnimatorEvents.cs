using UnityEngine;

namespace TacticalPanicCode
{
    public partial class AnimatorEvents : MonoBehaviour
    {
        public void Death()
        {
            gameObject.GetComponentInParent<UnitOperator>().DeathAnimationEnds();
        }
    }
}
