using UnityEngine;

namespace TacticalPanicCode
{
    public class AttackRadiusOperator : MonoBehaviour
    {
        [SerializeField]
        private UnitOperator unit;

        void OnTriggerEnter(Collider other)
        {
            unit.OnAttackRadiusEnter(other);
        }

        void OnTriggerExit(Collider other)
        {
            unit.OnAttackRadiusExit(other);
        }
    }
}
