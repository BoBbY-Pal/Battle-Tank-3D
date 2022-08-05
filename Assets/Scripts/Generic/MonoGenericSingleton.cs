using UnityEngine;

public class MonoGenericSingleton<T> : MonoBehaviour where T : MonoGenericSingleton<T>  //Generic Singleton
{
    public static T Instance { get; private set; }

    //Making Awake virtual, in case someone wants their custom awake.
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}