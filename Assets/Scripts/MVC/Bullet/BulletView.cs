using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BulletView : MonoBehaviour
{
    private BulletController _bulletController;
    
    public LayerMask tankMask;      // layer mask to detect if it's only Tank
    public ParticleSystem explosionParticles;
    public AudioSource explosionAudio;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, _bulletController.bulletModel.maxLifeTime);
    }

    private void Update()
    {   // Dropping bullet like a rocket instead of going straight.
        transform.forward = _rigidbody.velocity; 
    }

    public void SetBulletController(BulletController controller) => _bulletController = controller;
  

    private void OnTriggerEnter(Collider collision)
    {
        _bulletController.OnCollisionEnter(collision);
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
