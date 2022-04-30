using UnityEngine;
public class EnemyTankModel
{
    public int health { get; }
    public int damage { get; }
    public int speed { get; } 
    
    public EnemyTankModel(int damage, int speed, int health)
    {
        this.damage = damage;
        this.speed = speed;
        this.health = health;
    }

}
