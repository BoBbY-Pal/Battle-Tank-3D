using UnityEngine;
public class EnemyTankController
{
    private EnemyTankModel tankModel { get; }
    private EnemyTankView tankView { get; }
    
    public EnemyTankController(EnemyTankModel tankModel, EnemyTankView tankPrefab)
    {
        this.tankModel = tankModel;
        tankView = Object.Instantiate(tankPrefab);
    }

}
