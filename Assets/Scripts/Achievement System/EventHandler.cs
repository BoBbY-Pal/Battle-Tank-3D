using System;

public class EventHandler : MonoSingletonGeneric<EventHandler>
{
    public event Action<int> OnBulletFired;
    // public event Action OnEnemyDeath;

    public void InvokeOnBulletFired(int bulletCount)
    {
        OnBulletFired?.Invoke(bulletCount);
    }

    // public void InvokeOnEnemyDeath()
    // {
    //     OnEnemyDeath?.Invoke();
    // }

}