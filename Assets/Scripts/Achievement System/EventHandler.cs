using System;

public class EventHandler : MonoGenericSingleton<EventHandler>
{
    public event Action OnBulletFired;
    public event Action OnEnemyDeath;
    public event Action OnBulletCollision;
    public event Action OnFireButtonPressed;
    public event Action OnFireButtonReleased;

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
        OnBulletCollision?.Invoke();
    }
    
    public void InvokeFireButtonReleasedEvent()
    {
        OnFireButtonReleased?.Invoke();
    }
    
    public void InvokeFireButtonPressedEvent()
    {
        OnFireButtonPressed?.Invoke();
    }
    
}