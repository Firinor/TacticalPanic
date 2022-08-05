using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public abstract class UnitBehavior
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void HardExit() { }
        public virtual void Check() { }
        public virtual void Update() { }
        public virtual void FixUpdate() { }
    }
}
