using Interfaces;
using UnityEngine;

public class BulletController 
{
    public BulletModel bulletModel { get; }
    private BulletView bulletView { get;  }

    public BulletController(BulletModel bulletModel, BulletView bulletPrefab, float launchForce,
        Transform fireTransform)
    {
        this.bulletModel = bulletModel;
        bulletView = Object.Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation);
        bulletView.SetBulletController(this);
        bulletView.GetComponent<Rigidbody>().velocity = fireTransform.forward * launchForce;  // Applying velocity to bullet.
    }
    
    public void OnCollisionEnter(Collider other)
    {
        ApplyDamageToDamageable();
        PlayExplosionParticle();
        PlayExplosionSound();
        bulletView.DestroyBullet();
    }


    private void ApplyDamageToDamageable()
    {
        // find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(bulletView.transform.position, bulletModel.explosionRadius, bulletView.tankMask);

        foreach (var t in colliders)
        {
            IDamageable damageable = t.GetComponent<IDamageable>();
            if (damageable != null)
            {
                float damage = CalculateDamage(t.transform.position);
                damageable.TakeDamage(damage);
                
                // Applying ExplosionForce
                Rigidbody targetRigidbody = t.GetComponent<Rigidbody>();
                if (targetRigidbody)
                {
                    targetRigidbody.AddExplosionForce(bulletModel.explosionForce, bulletView.transform.position, bulletModel.explosionRadius);
                }
            }
        }
    }

    private float CalculateDamage(Vector3 targetPos)      // Calculates the damage based on the distance of explosion
    {
        
        float explosionDistance = (targetPos - bulletView.transform.position).magnitude;
        float relativeDistance = (bulletModel.explosionRadius - explosionDistance) / bulletModel.explosionRadius;
        float damage = relativeDistance * bulletModel.maxDamage;

        damage = Mathf.Max(0f, damage);
        return damage;
    }

    private void PlayExplosionParticle()
    {   // Decoupling explosion particle from bullet shell
        bulletView.explosionParticles.transform.parent = null;
        
        bulletView.explosionParticles.Play();
        bulletView.DestroyParticleSystem(bulletView.explosionParticles);
    }
    
    private void PlayExplosionSound()
    {
        bulletView.explosionAudio.Play();
    }
}
