using UnityEngine;

public class PlayerTankModel 
{
    public int speed { get; }
    public float health { get; }  
    public int maxHealth { get; }
    public int rotationRate { get; }
    public int turretRotationRate { get;}
    
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    
    public bool is_Dead { get; set; }

    public PlayerTankModel(int speed, int health, int rotationRate, int turretRotationRate)
    {
        is_Dead = false;
        maxHealth = health;
        this.health = health;
        this.rotationRate = rotationRate;
        this.turretRotationRate = turretRotationRate;
        this.speed = speed;
    }
}

