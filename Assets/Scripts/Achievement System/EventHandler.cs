using System;

public class EventHandler : MonoSingletonGeneric<EventHandler>
{
    public event Action<int> OnBulletFired;
    public event Action OnEnemyDeath;

    public void InvokeBulletFiredEvent(int bulletCount)
    {
        OnBulletFired?.Invoke(bulletCount);
    }

    public void InvokeEnemyDeathEvent()
    {
        OnEnemyDeath?.Invoke();
    }

}