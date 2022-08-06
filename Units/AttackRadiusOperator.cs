using UnityEngine;

namespace TacticalPanicCode
{
    public class AttackRadiusOperator : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Attack in! {GetHashCode()}");
        }

        void OnTriggerExit(Collider other)
        {
            Debug.Log($"Attack in! {GetHashCode()}");
        }
    }
}
