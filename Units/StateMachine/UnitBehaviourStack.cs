using System;
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

        #region MonoBehaviour
        private void Awake()
        {
            PushBehavior(new InfiniteIdleUnitBehaviour());
        }

        void Update()
        {
            currentBehavior.Update();
        }

        void FixedUpdate()
        {
            currentBehavior.FixedUpdate();
        }
        #endregion

        #region Stack
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
        #endregion

        #region UnitBehaviourFactory
        internal void CreatePathBehaviour(UnitOnLevelPathInformator path)
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

        internal void GoToTarget(UnitOperator target)
        {
            if (target == null)
                return;

            var ToTarget = new MoveToTargetUnitBehaviour(target, unit);
            PushBehavior(ToTarget);
        }

        internal void Attack()
        {
            UnitOperator target = unit.EnemyToAttack();
            if (target == null)
                return;

            var ToTarget = new FightUnitBehaviour(target, unit);
            PushBehavior(ToTarget);
        }
        #endregion
    }
}
