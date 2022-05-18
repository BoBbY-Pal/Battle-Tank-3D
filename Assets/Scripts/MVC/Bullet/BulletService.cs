using System;
using UnityEngine;
public class BulletService : MonoSingletonGeneric<BulletService>
{
    public BulletSOList bulletList;

    public void FireBullet(BulletType bulletType, Transform bulletTransform, float launchForce)
    {
        CreateBullet(bulletType, bulletTransform, launchForce);
    }

    private void CreateBullet(BulletType bulletType, Transform bulletTransform, float launchForce)
    {
        foreach (BulletSO bullet in bulletList.bulletTypeList)
        {
            if (bullet.bulletType == bulletType)
            {
                
            }
        }
    }
    
}
