using System;
using System.Collections.Generic;
using Unity.Mathematics;
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
            var distanceToGoalHolder = unit.distanceToGoal = new UnitDistanceToGoalHolder();

            Vector3 previewTarget = path.GetExitPoint();
            var ToPoint = new MoveToPointUnitBehaviour(previewTarget, unit);
            distanceToGoalHolder.AddSegment(ToPoint, 0f);
            PushBehavior(ToPoint);

            int length = path.points.Length;
            if (length > 0)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    if (path.points[i].delay > 0)
                    {
                        var Wait = new IdleSomeTimeUnitBehaviour(path.points[i].delay);
                        PushBehavior(Wait);
                    }

                    Vector3 target = path.GetPoint(i);
                    ToPoint = new MoveToPointUnitBehaviour(target, unit);
                    PushBehavior(ToPoint);

                    distanceToGoalHolder.AddSegment(ToPoint, math.distance(target, previewTarget));
                    previewTarget = target;
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
            if (!unit.IsThereAnyoneToAttack())
                return;

            var ToTarget = new FightUnitBehaviour(unit);
            PushBehavior(ToTarget);
        }
        #endregion
    }

    public class UnitDistanceToGoalHolder
    {
        private LinkedList<DistanceOfBehaviour> unitBehaviours = new LinkedList<DistanceOfBehaviour>();

        private float distanceToGoal;

        public void AddSegment(MoveToPointUnitBehaviour behaviour, float distance)
        {
            distanceToGoal += distance;
            unitBehaviours.AddLast(new DistanceOfBehaviour(behaviour, distance));
        }

        public void RemoveSegment()
        {
            if (unitBehaviours.Count == 0)
                return;

            distanceToGoal -= unitBehaviours.Last.Value.distanceOfGoal;
            unitBehaviours.RemoveLast();
        }

        public float DistanceToGoal(Vector3 currentPosition)
        {
            return distanceToGoal + math.distance(currentPosition, unitBehaviours.Last.Value.unitBehaviour.target);
        }

        private class DistanceOfBehaviour
        {
            public MoveToPointUnitBehaviour unitBehaviour;
            public float distanceOfGoal;

            public DistanceOfBehaviour(MoveToPointUnitBehaviour unitBehaviour, float distanceOfGoal)
            {
                this.unitBehaviour = unitBehaviour;
                this.distanceOfGoal = distanceOfGoal;
            }
        }
    }
}
