using UnityEngine;

public class SinglBehaviour<T> : MonoBehaviour
{
    public static T instance;

    public void SingletoneCheck(T instance)
    {
        if(instance == null)
            Destroy(gameObject);
        SinglBehaviour<T>.instance = instance;
    }
}
