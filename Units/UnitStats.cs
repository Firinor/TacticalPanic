using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class UnitStats : MonoBehaviour
    {
        private UnitBasis unitBasis;

        public GistOfUnit[] GistsOfUnit { get; }
        public GistBasis[] GistBasis => unitBasis.GistBasis;

        public bool IsAlive;

        public float Speed { get => unitBasis.mspeed; }
        public float CurrentHP;
    }
}
