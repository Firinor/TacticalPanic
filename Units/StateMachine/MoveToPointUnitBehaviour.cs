using System;
using UnityEngine;

namespace TacticalPanicCode.UnitBehaviours
{
    public class MoveToPointUnitBehaviour : UnitBehaviour
    {
        public Vector3 target { get; private set; }
        private Transform skinRoot;
        private Transform unitTransform;
        private Rigidbody rigidbody;
        private UnitOperator unitOperator;
        private float timeToCheckTarget = .5f;
        private float timer = 0;

        public float distanceToPoint;

        public MoveToPointUnitBehaviour(Vector3 target, UnitOperator unitOperator)
        {
            this.target = target;
            this.unitOperator = unitOperator;
            skinRoot = unitOperator.SkinRoot;
            unitTransform = unitOperator.transform;
            rigidbody = unitOperator.rigidbody;
        }
        public override void FixedUpdate()
        {
            rigidbody.AddRelativeForce(Vector3.forward * unitOperator.unitStats.Speed, ForceMode.Impulse);
            timer -= Time.fixedDeltaTime;
            if(timer < 0)
            {
                timer = timeToCheckTarget;
                LookAtPoint();
            }
            if (UnitOnTarget())
                Exit();
        }

        public override void Exit()
        {
            unitOperator.distanceToGoal?.RemoveSegment();
        }

        private void LookAtPoint()
        {
            unitTransform.LookAt(target);
            CorrectSkinRootRotation();
        }
        private void CorrectSkinRootRotation()
        {
            skinRoot.localRotation = Quaternion.Euler(0f, -unitTransform.rotation.eulerAngles.y, 0f);
        }
        private bool UnitOnTarget()
        {
            return (int)(unitTransform.position.x * 2 + 0.5f) == (int)(target.x * 2)
                && (int)(unitTransform.position.z * 2 + 0.5f) == (int)(target.z * 2);//y
        }
    }
}
