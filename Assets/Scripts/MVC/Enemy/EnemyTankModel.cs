using Enums;
using Scriptable_Objects.EnemySO;
using UnityEngine;
public class EnemyTankModel
{
    public float maxHealth { get; }
    public float currentHealth { get; set; }
    public float movementSpeed { get; }
    public float rotationSpeed { get;  }
    public float turretRotationRate { get; }
    public float minLaunchForce {get; }
    public float maxLaunchForce {get; }

    public bool b_IsDead { get; set; }
    public bool b_IsFired { get; set; }
    
    public Vector3 walkPoint {get; set;}
    public float walkPointRange {get; set;}
    public bool b_WalkPoint {get; set;}

    public float fireRate {get; set;}
    public float patrollingRange {get; set;}
    public float patrolTime { get; }
    public float attackRange {get; set;}
    public bool b_PlayerInRange {get; set;}
    public bool b_PlayerInAttackRange {get; set;}
    
    public Color tankColor { get; set; }
    public Color fullHealthColor { get; }
    public Color zeroHealthColor { get; }
        
    public EnemyType enemyType { get; }
    public BulletType bulletType { get; set;}
        
    
    public EnemyTankModel(EnemySO enemyInfo)
    {
        
        currentHealth = enemyInfo.health;
        maxHealth = enemyInfo.health;
        movementSpeed = enemyInfo.movementSpeed;
        rotationSpeed = enemyInfo.rotationSpeed;
        turretRotationRate = enemyInfo.turretRotationRate;
        
        b_WalkPoint = false;
        walkPointRange = enemyInfo.walkPointRange;
        patrolTime = enemyInfo.patrolTime;
        patrollingRange = enemyInfo.patrollingRange;
        attackRange = enemyInfo.attackRange;

        tankColor = enemyInfo.tankColor;
        fullHealthColor = Color.green;
        zeroHealthColor = Color.red;

        minLaunchForce = enemyInfo.minLaunchForce;
        maxLaunchForce = enemyInfo.maxLaunchForce;
        fireRate = enemyInfo.fireRate;
        bulletType = enemyInfo.bulletType;
        enemyType = enemyInfo.enemyType;
    }

    
}
