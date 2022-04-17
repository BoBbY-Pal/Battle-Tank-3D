using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankController<T> : MonoBehaviour where T : TankController<T>  //Generic Singleton
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

public class PlayerTankController : TankController<PlayerTankController>
{
    protected override void Awake()
    {
        base.Awake();
        //Custom Awake
    }

    public void MovePlayer()
    {
        
    }
}
public class EnemyTankController : MonoBehaviour
{
    private void Start()
    {
        PlayerTankController.Instance.MovePlayer();
    }
}
