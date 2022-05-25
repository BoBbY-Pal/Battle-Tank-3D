using System;
using Scriptable_Objects.BulletSO;
using Scriptable_Objects.TankSO;
using UnityEngine;

public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
{
    public EnemyTankView tankView;
    public TankScriptableObjectList enemyTankList;
    public BulletSOList bulletList;

    public EnemyTankController tankController;

    private void Start()
    {
       tankController = CreateEnemyTank();
    }
    // this commented code was used for creating a tank but right now i'm changing the logic
    private EnemyTankController CreateEnemyTank()
    {
        EnemyTankModel model = new EnemyTankModel(30, 15, 100);
        EnemyTankController tank = new EnemyTankController(model, tankView);
        return tank;
    }
}