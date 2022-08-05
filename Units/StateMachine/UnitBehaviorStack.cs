using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class UnitBehaviorStack : MonoBehaviour
    {
        private UnitBehavior currentBehavior;
        private Stack<UnitBehavior> stack;

        private void Awake()
        {
            stack.Push(new InfiniteIdleUnitBehavior());
        }

        public void PushBehavior(UnitBehavior behavior)
        {
            stack.Push(behavior);
            currentBehavior = behavior;
            currentBehavior.Enter();
        }

        private void PopBehavior(UnitBehavior behavior)
        {
            if (behavior != stack.Peek())
                return;

            stack.Pop().Exit();
            currentBehavior = stack.Peek();
        }

        void Update()
        {
            currentBehavior.Update();
        }
    }
}
