using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public static class InputMouseInformator
    {
        public static GameObject MouseTarget;

        public static float TextScrollSensivity;
        public static float ZoomScrollSensivity;

        private static int mouseLayer = 0;

        public static int MouseLayer
        {
            get { return mouseLayer; }
            set { mouseLayer = math.clamp(value, 0, 1); }
        }
    }
}
