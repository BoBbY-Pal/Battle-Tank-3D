using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BulletView : MonoBehaviour
{
    private BulletController _bulletController;
    
    public LayerMask tankMask;      // layer mask to detect if it's only Tank
    
   
    public ParticleSystem explosionParticle;
    
  
    public AudioSource explosionAudio;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {   
        // Bullet will Drop like a rocket instead of just going straight.
        transform.forward = _rigidbody.velocity; 
    }
    
    public void Initialise(BulletController controller)
    {
        _bulletController = controller;
        // explosionParticle = particleEffect;
        // explosionAudio = particleEffect.GetComponent<AudioSource>();
        StartCoroutine(_bulletController.InvokeExplode()); 
    }
        

    private void OnTriggerEnter(Collider other)
    {
        EventHandler.Instance.InvokeBulletCollidedEvent();
    }
    
    public void DisableParticleObject()
    {
        float waitTime = Mathf.Max(explosionParticle.main.duration,
                                   explosionAudio.clip.length);

        StartCoroutine(BulletService.Instance.ReturnBulletToPool(_bulletController, waitTime));
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
