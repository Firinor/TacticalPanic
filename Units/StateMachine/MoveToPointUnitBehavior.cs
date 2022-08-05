using UnityEngine;

namespace TacticalPanicCode
{
    public class MoveToPointUnitBehavior : UnitBehavior
    {
        private Vector3 target;
        private Transform skinRoot;
        private Transform unitTransform;
        private Rigidbody rigidbody;
        private UnitOperator unitOperator;

        public MoveToPointUnitBehavior(Vector3 target, UnitOperator unitOperator)
        {
            this.target = target;
            this.unitOperator = unitOperator;
            skinRoot = unitOperator.skinRoot;
            unitTransform = unitOperator.transform;
            rigidbody = unitOperator.rigidbody;
        }
        public override void Enter()
        {
            unitTransform.LookAt(target);
            CorrectSkinRootRotation();
        }
        public override void Update()
        {
            rigidbody.AddRelativeForce(Vector3.forward * unitOperator.speed/100, ForceMode.Impulse);
            if (UnitOnTarget())
                Exit();
        }
        private void CorrectSkinRootRotation()
        {
            skinRoot.localRotation = Quaternion.Euler(0f, -unitTransform.rotation.eulerAngles.y, 0f);
        }
        private bool UnitOnTarget()
        {
            return Mathf.Floor(unitTransform.position.x * 2 + 0.5f) == Mathf.Floor(target.x * 2)
                && Mathf.Floor(unitTransform.position.z * 2 + 0.5f) == Mathf.Floor(target.z * 2);//y
        }
    }
}
