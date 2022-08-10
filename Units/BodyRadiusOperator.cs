using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class BodyRadiusOperator : MonoBehaviour
    {
        [SerializeField]
        private UnitOperator unit;

        void OnTriggerEnter(Collider other)
        {
            unit.OnBodyRadiusEnter(other);
        }

        void OnTriggerExit(Collider other)
        {
            unit.OnBodyRadiusExit(other);
        }
    }
}
