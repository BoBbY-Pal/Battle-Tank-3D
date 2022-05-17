using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BulletView : MonoBehaviour
{
    private BulletController _bulletController;
    
    public LayerMask tankMask;      // layer mask to detect if it's only Tank
    public ParticleSystem explosionParticles;
    public AudioSource explosionAudio;
    
    private void Start()
    {
        Destroy(gameObject,_bulletController.bulletModel.maxLifeTime);
    }
    
    public void SetTankControllerReference(BulletController controller)
    {
        _bulletController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        _bulletController.OnCollisionEnter(other);
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    public void DestroyParticleSystem(ParticleSystem particles)
    {
        Destroy(particles.gameObject, particles.main.duration);
    }
}
