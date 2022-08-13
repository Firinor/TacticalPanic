using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class AgroRadiusOperator : MonoBehaviour
    {
        [SerializeField]
        private UnitOperator unit;
        [SerializeField]
        private Collider agroCollider;

        public void OnTriggerEnter(Collider other)
        {
            unit.OnAgroRadiusEnter(other);
        }
        public void OnTriggerExit(Collider other)
        {
            unit.OnAgroRadiusExit(other);
        }

        public void SetColliderActivity(bool flag)
        {
            agroCollider.enabled = flag;
        }
    }
}
