using System;
using UnityEngine;

public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
{
    public EnemyTankView tankView;
    public TankScriptableObjectList enemyTankList;
    public BulletScriptableObjectList bulletList;

    private void Start()
    {
        CreateEnemyTank();
    }

    private EnemyTankController CreateEnemyTank()
    {
        EnemyTankModel model = new EnemyTankModel(30, 15, 100);
        EnemyTankController tank = new EnemyTankController(model, tankView);
        return tank;
    }
}