using System;

public class EventHandler : MonoGenericSingleton<EventHandler>
{
    public event Action OnBulletFired;
    public event Action OnEnemyDeath;
    public event Action BulletCollided;

    public void InvokeBulletFiredEvent()
    {
        OnBulletFired?.Invoke();
    }

    public void InvokeEnemyDeathEvent()
    {
        OnEnemyDeath?.Invoke();
    }

    public void InvokeBulletCollidedEvent()
    {
        BulletCollided?.Invoke();
    }

}