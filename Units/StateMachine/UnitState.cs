using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public enum UnitState { Idle, Move, Attack, Death }

    public abstract class BaseUnitState : ScriptableObject
    {
        public virtual void Start() { }
        public virtual void Stop() { }
        public virtual void Update() { }
        public virtual void FixUpdate() { }
    }
}
