using Enums;
using Scriptable_Objects.EnemySO;
using UnityEngine;

public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
{
    public EnemyTankView tankView;
    public EnemySOList enemyTankList;

    public EnemyTankController tankController;
    private EnemyType enemyTankType;

    private void Start()
    {
        enemyTankType = (EnemyType)Mathf.Floor(Random.Range(0, 2.5f)); 
        tankController = CreateEnemyTank(enemyTankType);
    }
    
    private EnemyTankController CreateEnemyTank(EnemyType tankType)
    {
        foreach (EnemySO enemyTank in enemyTankList.enemies)
        {
            if (enemyTank.enemyType == enemyTankType)
            {
                EnemyTankModel model = new EnemyTankModel(enemyTankList.enemies[(int) tankType]);
                EnemyTankController tank = new EnemyTankController(model, tankView);
                return tank;
            }

        }

        return null;
    }
    
}