using UnityEngine;


public class BulletModel
{
    public float maxDamage { get; }
    public float explosionRadius { get; }
    public float maxLifeTime { get; }
    public float explosionForce { get; }

    public BulletModel(float maxDamage, float explosionRadius, float maxLifeTime, float explosionForce)
    { 
        this.maxDamage = maxDamage;
        this.explosionRadius =explosionRadius;
        this.maxLifeTime = maxLifeTime;
        this.explosionForce = explosionForce;
    }
}
