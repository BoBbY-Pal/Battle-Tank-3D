using UnityEngine;

public class PlayerTankModel 
{
    public int speed { get; }       // Movement speed
    public float currentHealth { get; set; }  
    public int maxHealth { get; }
    public int rotationRate { get; }  // Turning speed
    public int turretRotationRate { get;}
    public float pitchRange = 0.2f;     // For transition between sound so that it can be heard smoothly.
    public BulletType bulletType { get; }  
    
    // Bullet
    public float chargeSpeed { get; }
    public float maxChargeTime { get; }
    public float minLaunchForce { get; }
    public float currentLaunchForce { get; set; }
    public float maxLaunchForce { get; }
    
    // Health UI
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    
    public bool isDead { get; set; }
    public bool isFired { get; set; }

    public PlayerTankModel (int speed, int currentHealth, int rotationRate, int turretRotationRate, 
                            float maxLaunchForce, float minLaunchForce, float maxChargeTime, BulletType bulletType)
    {
        isDead = false;
        isFired = false;
        maxHealth = currentHealth;
        this.currentHealth = currentHealth;
        this.rotationRate = rotationRate;
        this.turretRotationRate = turretRotationRate;
        currentLaunchForce = minLaunchForce;
        this.maxLaunchForce = maxLaunchForce;
        this.minLaunchForce = minLaunchForce;
        this.maxChargeTime = maxChargeTime;
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        this.bulletType = bulletType;
        this.speed = speed;
    }
}

