using UnityEngine;

namespace TacticalPanicCode.UnitBehaviours
{
    public class MoveToTargetUnitBehaviour : UnitBehaviour
    {
        private Transform skinRoot;
        private Transform unitTransform;
        private Transform targetTransform;
        private Rigidbody rigidbody;
        private UnitStats unitStats;
        private float timeToCheckTarget = .5f;
        private float timer = 0;

        public MoveToTargetUnitBehaviour(UnitOperator target, UnitOperator unitOperator)
        {
            unitStats = unitOperator.Stats;
            skinRoot = unitOperator.SkinRoot;
            unitTransform = unitOperator.transform;
            targetTransform = target.transform;
            rigidbody = unitOperator.unitRigidbody;
        }
        public override void FixedUpdate()
        {
            rigidbody.AddRelativeForce(Vector3.forward * unitStats.Speed, ForceMode.Impulse);
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                timer = timeToCheckTarget;
                LookAtTarget();
            }
            if (UnitOnTarget())
                Exit();
        }
        private void LookAtTarget()
        {
            unitTransform.LookAt(targetTransform);
            CorrectSkinRootRotation();
        }
        private void CorrectSkinRootRotation()
        {
            skinRoot.localRotation = Quaternion.Euler(0f, -unitTransform.rotation.eulerAngles.y, 0f);
        }
        private bool UnitOnTarget()
        {
            return (int)(unitTransform.position.x * 2 + 0.5f) == (int)(targetTransform.position.x  * 2)
                && (int)(unitTransform.position.z * 2 + 0.5f) == (int)(targetTransform.position.z * 2);//y
        }
    }
}
