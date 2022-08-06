using Scriptable_Objects.BulletSO;
using UnityEngine;

public class BulletModel
{
    public float MaxDamage { get; }
    public float ExplosionRadius { get; }
    public float MaxLifeTime { get; }
    public float ExplosionForce { get; }
    public Transform BulletTransform { get; }
    public float LaunchForce { get; }
    

    public BulletModel(BulletSO bulletInfo, Transform bulletTransform, float launchForce)
    {
        BulletTransform = bulletTransform;
        LaunchForce = launchForce;

        MaxDamage = bulletInfo.damage;
        ExplosionRadius = bulletInfo.explosionRadius;
        MaxLifeTime = bulletInfo.maxLifeTime;
        ExplosionForce = bulletInfo.explosionForce;
    }
}
