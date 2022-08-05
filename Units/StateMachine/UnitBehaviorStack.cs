using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode
{
    public class UnitBehaviorStack : MonoBehaviour
    {
        private UnitBehavior currentBehavior;
        private Stack<UnitBehavior> stack = new Stack<UnitBehavior>();
        [SerializeField]
        private UnitOperator unit;

        private void Awake()
        {
            PushBehavior(new InfiniteIdleUnitBehavior());
        }

        public void PushBehavior(UnitBehavior behavior)
        {
            behavior.exitEvent += PopBehavior;
            stack.Push(behavior);
            currentBehavior = behavior;
            currentBehavior.Enter();
        }

        public void PopBehavior(UnitBehavior behavior)
        {
            if (behavior != stack.Peek())
                return;

            PopBehavior();
        }
        private void PopBehavior()
        {
            stack.Pop();
            currentBehavior = stack.Peek();
        }

        void Update()
        {
            currentBehavior.Update();
        }

        void FixedUpdate()
        {
            currentBehavior.FixedUpdate();
        }

        internal void CreateBehaviour(UnitOnLevelPathInformator path)
        {
            Vector3 target;
            int length = path.points.Length;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    target = path.GetPoint(i);
                    var ToPoint = new MoveToPointUnitBehavior(target, unit);
                    PushBehavior(ToPoint);

                    if(path.points[i].delay > 0)
                    {
                        var Wait = new IdleSomeTimeUnitBehavior(path.points[i].delay);
                        PushBehavior(Wait);
                    }
                }
            }
            target = path.GetExitPoint();
            var ToExit = new MoveToPointUnitBehavior(target, unit);
            PushBehavior(ToExit);
        }
    }
}
