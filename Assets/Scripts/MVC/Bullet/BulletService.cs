using System.Collections;
using Enums;
using Scriptable_Objects.BulletSO;
using UnityEngine;

public class BulletService : MonoGenericSingleton<BulletService>
{
    [Tooltip("Bullet game object with BulletView script attached to it.")]
    public BulletView bulletView;
    
    [Tooltip("Particle effect that will be played when bullet hits something.")]
    public ParticleSystem bulletExplosionEffect;
    
    [Tooltip("List of bullet scriptable objects.")]
    public BulletSOList bulletsList;
    
    private ServicePoolBullet  _bulletPool;

    private void Start()
    {
        _bulletPool = GetComponent<ServicePoolBullet>();
    }

    public BulletController CreateBullet(BulletType bulletType, Transform bulletTransform, float launchForce)
    {
        
        foreach (BulletSO bullet in bulletsList.bullets)
        {
            if (bullet.bulletType == bulletType)
            {
                BulletModel bulletModel = new BulletModel(bulletsList.bullets[(int)bulletType],
                                                          bulletTransform, launchForce);
                BulletController bulletController = _bulletPool.GetBullet(bulletModel, bulletView);
                bulletController.Enable(bulletTransform, launchForce);
                return bulletController;
            }
        }

        return null;
    }
    
    public IEnumerator ReturnBulletToPool(BulletController bulletController, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        // Disable the object and return it to the pool.
        bulletController.Disable();
        _bulletPool.ReturnItem(bulletController);
    }
    
}
