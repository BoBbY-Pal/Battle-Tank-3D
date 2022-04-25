using UnityEngine;

public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>  //Generic Singleton
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    //Making Awake virtual, in case someone wants their custom awake.
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}