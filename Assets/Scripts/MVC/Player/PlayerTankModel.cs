using Enums;
using UnityEngine;

public class PlayerTankModel 
{
    public int Speed { get; }       // Movement speed
    public float CurrentHealth { get; set; }  
    public int MaxHealth { get; }
    public int RotationRate { get; }  // Turning speed
    public int TurretRotationRate { get;}
    public float pitchRange = 0.2f;     // For transition between sound so that it can be heard smoothly.
    public BulletType BulletType { get; }  
    
    // Bullet
    public float ChargeSpeed { get; }
    public float MaxChargeTime { get; }
    public float MinLaunchForce { get; }
    public float CurrentLaunchForce { get; set; }
    public float MaxLaunchForce { get; }
    
    // Health UI
    public Color FullHealthColor {get;}
    public Color ZeroHealthColor {get;}
    
    public bool IsDead { get; set; }
    public bool b_IsFired { get; set; }
    public int BulletsFired { get; set; }
    public int EnemiesKilled { get; set; }

    public PlayerTankModel (int speed, int currentHealth, int rotationRate, int turretRotationRate, 
                            float maxLaunchForce, float minLaunchForce, float maxChargeTime, BulletType bulletType)
    {
        IsDead = false;
        b_IsFired = false;
        MaxHealth = currentHealth;
        CurrentHealth = currentHealth;
        RotationRate = rotationRate;
        TurretRotationRate = turretRotationRate;
        CurrentLaunchForce = minLaunchForce;
        MaxLaunchForce = maxLaunchForce;
        MinLaunchForce = minLaunchForce;
        MaxChargeTime = maxChargeTime;
        ChargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        BulletType = bulletType;
        Speed = speed;
        FullHealthColor = Color.green;
        ZeroHealthColor = Color.red;
    }
}

