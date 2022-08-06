using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class AgroRadiusOperator : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Agro in! {GetHashCode()}");
        }
        public void OnTriggerExit(Collider other)
        {
            Debug.Log($"Agro out! {GetHashCode()}");
        }
    }
}
