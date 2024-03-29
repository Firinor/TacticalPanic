using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class InputOperator : MonoBehaviour
    {
        public float TextScrollSensivity = 1;
        public float ZoomScrollSensivity = 1;

        [SerializeField]
        private Material defaultMaterial;
        [SerializeField]
        private Material pickMaterial;

        public static Material DefaultMaterial { get; set; }
        public static Material PickMaterial { get; set; }

        public void Awake()
        {
            DefaultMaterial = defaultMaterial;
            PickMaterial = pickMaterial;
            InputMouseInformator.TextScrollSensivity = TextScrollSensivity;
            InputMouseInformator.ZoomScrollSensivity = ZoomScrollSensivity;
        }
    }
}
