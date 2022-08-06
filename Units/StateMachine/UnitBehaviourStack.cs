using System.Collections.Generic;
using UnityEngine;

namespace TacticalPanicCode.UnitBehaviours
{
    public class UnitBehaviourStack : MonoBehaviour
    {
        private UnitBehaviour currentBehavior;
        private Stack<UnitBehaviour> stack = new Stack<UnitBehaviour>();
        [SerializeField]
        private UnitOperator unit;

        private void Awake()
        {
            PushBehavior(new InfiniteIdleUnitBehaviour());
        }

        public void PushBehavior(UnitBehaviour behavior)
        {
            behavior.exitEvent += PopBehavior;
            stack.Push(behavior);
            currentBehavior = behavior;
            currentBehavior.Enter();
        }

        public void PopBehavior(UnitBehaviour behavior)
        {
            if (behavior != stack.Peek())
                return;

            PopBehavior();
        }
        private void PopBehavior()
        {
            stack.Pop();
            currentBehavior = stack.Peek();
            currentBehavior.Enter();
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
            //from end to begin
            Vector3 target = path.GetExitPoint();
            var ToExit = new MoveToPointUnitBehaviour(target, unit);
            PushBehavior(ToExit);

            int length = path.points.Length;
            if (length > 0)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    if(path.points[i].delay > 0)
                    {
                        var Wait = new IdleSomeTimeUnitBehaviour(path.points[i].delay);
                        PushBehavior(Wait);
                    }
                    target = path.GetPoint(i);
                    var ToPoint = new MoveToPointUnitBehaviour(target, unit);
                    PushBehavior(ToPoint);
                }
            }
        }
    }
}
