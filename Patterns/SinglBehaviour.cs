using UnityEngine;

namespace TacticalPanicCode
{
    public class SinglBehaviour<T> : MonoBehaviour
    {
        public static T instance;

        public void SingletoneCheck(T instance)
        {
            if (SinglBehaviour<T>.instance != null)
                Destroy(gameObject);
            SinglBehaviour<T>.instance = instance;
        }
    }
}
