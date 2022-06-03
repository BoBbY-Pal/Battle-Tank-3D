using UnityEngine;
using System;

public class EventHandler : MonoSingletonGeneric<EventHandler>
{
    public event Action OnBulletFired;
    // public event Action OnEnemyDeath;

    public void InvokeOnBulletFired()
    {
        OnBulletFired?.Invoke();
    }

    // public void InvokeOnEnemyDeath()
    // {
    //     OnEnemyDeath?.Invoke();
    // }

}