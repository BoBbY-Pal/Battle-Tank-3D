using UnityEngine;

public class PlayerTankModel 
{
    public int speed { get; }       // Movement speed
    public float health { get; set; }  
    public int maxHealth { get; }
    public int rotationRate { get; }  // Turning speed
    public int turretRotationRate { get;}
    
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    
    public bool isDead { get; set; }

    public PlayerTankModel(int speed, int health, int rotationRate, int turretRotationRate)
    {
        isDead = false;
        maxHealth = health;
        this.health = health;
        this.rotationRate = rotationRate;
        this.turretRotationRate = turretRotationRate;
        this.speed = speed;
    }
}

