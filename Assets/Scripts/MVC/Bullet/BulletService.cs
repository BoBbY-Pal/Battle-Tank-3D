using Enums;
using Scriptable_Objects.BulletSO;
using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    public BulletSOList bulletList;

    public void SetBulletProperties(BulletType bulletType, Transform bulletTransform, float launchForce)
    {
        CreateBullet(bulletType, bulletTransform, launchForce);
    }

    private BulletController CreateBullet(BulletType bulletType, Transform bulletTransform, float launchForce)
    {
        foreach (BulletSO bullet in bulletList.bulletTypeList)
        {
            if (bullet.bulletType == bulletType)
            {
                BulletModel bulletModel = new BulletModel(bulletList.bulletTypeList[(int)bulletType].damage,
                    bulletList.bulletTypeList[(int)bulletType].explosionRadius,
                    bulletList.bulletTypeList[(int)bulletType].maxLifeTime,
                    bulletList.bulletTypeList[(int)bulletType].explosionForce);

                BulletController bulletController =
                    new BulletController(bulletModel, bulletList.bulletTypeList[(int) bulletType].bulletView,
                        launchForce, bulletTransform);
                return bulletController;
            }
        }

        return null;
    }
    
}
