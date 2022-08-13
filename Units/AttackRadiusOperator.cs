using UnityEngine;

namespace TacticalPanicCode
{
    public class AttackRadiusOperator : MonoBehaviour
    {
        [SerializeField]
        private UnitOperator unit;
        [SerializeField]
        private Collider attackCollider;
        [SerializeField]
        private SpriteRenderer rangeSprite;

        void OnTriggerEnter(Collider other)
        {
            unit.OnAttackRadiusEnter(other);
        }

        void OnTriggerExit(Collider other)
        {
            unit.OnAttackRadiusExit(other);
        }
        public void SetColliderActivity(bool flag)
        {
            attackCollider.enabled = flag;
        }
        public void SetVisualRangeActivity(bool flag)
        {
            rangeSprite.enabled  = flag;
        }
    }
}
