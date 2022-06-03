using Enums;
using UnityEngine;

namespace Scriptable_Objects.EnemySO
{
    [ CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy/NewScriptableObject") ]
    public class EnemySO : ScriptableObject
    {
        [Header("Enemy Type")] public EnemyType enemyType;

        [Header("Enemy Prefab")] public Color tankColor;

        [Header("Health Parameter")] public float health;

        [Header("Movement Parameters")] 
        public float movementSpeed;
        public float rotationSpeed;
        public float turretRotationRate;
        public float walkPointRange;
        public float patrollingRange;
        public float patrolTime;

        [Header("Attack Parameters")] 
        public BulletType bulletType;
        public float fireRate;
        public float attackRange;
        public float maxLaunchForce;
        public float minLaunchForce;
    }
}
