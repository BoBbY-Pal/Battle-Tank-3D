using System.Collections;
using Interfaces;
using UnityEngine;

public class BulletController 
{
    private BulletModel BulletModel { get; }
    private BulletView BulletView { get;  }

    private Rigidbody _rigidbody;
    public BulletController(BulletModel bulletModel, BulletView bulletPrefab)
    {
        
        BulletModel = bulletModel;
        BulletView = Object.Instantiate(bulletPrefab, bulletModel.BulletTransform.position, bulletModel.BulletTransform.rotation);
        BulletView.Initialise(this);
        
        // Applying velocity to bullet.
        _rigidbody = BulletView.GetComponent<Rigidbody>();
        _rigidbody.velocity = bulletModel.BulletTransform.forward * bulletModel.LaunchForce;
    }

    private void SubscribeEvents()
    {
        EventHandler.Instance.BulletCollided += ApplyDamageToDamageable;
        EventHandler.Instance.BulletCollided += Explode;
    }

    private void UnSubscribeEvents()
    {
        EventHandler.Instance.BulletCollided -= ApplyDamageToDamageable;
        EventHandler.Instance.BulletCollided -= Explode;
        
    }


    private void ApplyDamageToDamageable()
    {
        // find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(BulletView.transform.position, BulletModel.ExplosionRadius, BulletView.tankMask);

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
                    targetRigidbody.AddExplosionForce(BulletModel.ExplosionForce, BulletView.transform.position, BulletModel.ExplosionRadius);
                }
            }
        }
    }

    private float CalculateDamage(Vector3 targetPos)   
    {
        
        float explosionDistance = (targetPos - BulletView.transform.position).magnitude;
        float relativeDistance = (BulletModel.ExplosionRadius - explosionDistance) / BulletModel.ExplosionRadius;
        float damage = relativeDistance * BulletModel.MaxDamage;

        damage = Mathf.Max(0f, damage);
        return damage;
    }

    public IEnumerator InvokeExplode()
    {
        // If bullet doesn't hit anything then it's will be exploded after it's lifetime.
        yield return new WaitForSeconds(BulletModel.MaxLifeTime);
        Explode();
    }
    
    private void Explode()
    {
        BulletView.explosionParticle.gameObject.SetActive(true);
        
        // Decoupling explosion particle from bullet shell
        // BulletView.explosionParticle.transform.parent = null;
        
        BulletView.explosionParticle.Play();
        BulletView.explosionAudio.Play();
        
        BulletView.DisableParticleObject();
    }


    public void Disable()
    {
        BulletView.Disable();
        UnSubscribeEvents();
    }
    public void Enable(Transform bulletTransform, float launchForce)
    {
        // Changing bullet position at the recent turret location.
        var transform = BulletView.transform;
        transform.position = bulletTransform.position;
        transform.rotation = bulletTransform.rotation;
        _rigidbody.velocity = bulletTransform.forward * launchForce;
        
        // Enable the object and subscribe to events.
        BulletView.Enable();
        SubscribeEvents();
    }
}
