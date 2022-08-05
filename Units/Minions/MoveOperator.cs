using System;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace TacticalPanicCode
{
    public class MoveOperator : MonoBehaviour
    {
        [SerializeField]
        private float speed = 3f;
        private Vector3 target;
        private UnitOnLevelPathInformator path;

        [SerializeField]
        private new Rigidbody rigidbody;
        [SerializeField]
        private Transform skinRoot;

        [SerializeField]
        private bool moveOn;
        [SerializeField]
        private UnitOnLevelPathInformator.WayPoint targetPoint;

        [HideInInspector]
        private new Transform transform;

        private ReactiveProperty<Quaternion> quaternionReactiveProperty = new ReactiveProperty<Quaternion>();
        private CompositeDisposable disposables = new CompositeDisposable();

        void Awake()
        {
            transform = base.transform;

            IObservable<Unit> streamToForse = this.FixedUpdateAsObservable().Where(_ => moveOn);
            streamToForse.Subscribe(_ => ForseToPoint()).AddTo(disposables);
            IObservable<Unit> streamToCorrectSkinRoot = this.UpdateAsObservable().Where(_ => CheckSkinRootRotation());
            streamToCorrectSkinRoot.Subscribe(_ => CorrectSkinRootRotation()).AddTo(disposables);
        }

        ~MoveOperator()
        {
            disposables.Clear();
        }

        private bool CheckSkinRootRotation()
        {
            return skinRoot.localRotation.y != -transform.rotation.eulerAngles.y;
        }

        private void CorrectSkinRootRotation()
        {
            skinRoot.localRotation = Quaternion.Euler(0f, -transform.rotation.eulerAngles.y, 0f);
        }

        private void ForseToPoint()
        {
            if (moveOn)
            {
                transform.LookAt(target);
                CorrectSkinRootRotation();
                rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
                //,target;
                if (UnitOnTarget())
                {
                    moveOn = false;
                }
            }
        }

        private bool UnitOnTarget()
        {
            return (int)(transform.position.x * 2 + 0.5f) == (int)(target.x * 2)
                && (int)(transform.position.z * 2 + 0.5f) == (int)(target.z * 2);//y
        }

        public void Deactivate()
        {
            enabled = false;
        }

        internal void SpawnToPoint(Vector3 point)
        {
            gameObject.transform.position = point;
        }

        internal IEnumerator FollowThPath(UnitOnLevelPathInformator enemyPath)
        {
            path = enemyPath;
            SpawnToPoint(path.GetSpawnPoint());
            yield return new WaitForSeconds(0.5f);
            int length = path.points.Length;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    targetPoint = path.points[i];
                    target = path.GetPoint(i);
                    moveOn = true;
                    while (moveOn)
                        yield return null;
                    yield return new WaitForSeconds(path.points[i].delay);
                }
            }
            targetPoint = new UnitOnLevelPathInformator.WayPoint();
            target = path.GetExitPoint();
            moveOn = true;
            StopCoroutine("FollowThPath");
        }
    }
}