using System;
using Enums;
using Random = UnityEngine.Random;
using Scriptable_Objects.EnemySO;

public class EnemyTankService : MonoGenericSingleton<EnemyTankService>
{
    public EnemyTankView tankView;
    public EnemySOList enemyTankList;
    public EnemyTankController tankController;

    private EnemyType enemyTankType;

    private void Start()
    {
        int lengthOfEnum = Enum.GetValues(typeof(EnemyType)).Length;
        enemyTankType = (EnemyType) Random.Range(0, lengthOfEnum);
        tankController = CreateEnemyTank();
    }
    
    private EnemyTankController CreateEnemyTank()
    {
        foreach (EnemySO enemyTank in enemyTankList.enemies)
        {
            if (enemyTank.enemyType == enemyTankType)
            {
                EnemyTankModel model = new EnemyTankModel(enemyTankList.enemies[(int) enemyTankType]);
                EnemyTankController tank = new EnemyTankController(model, tankView);
                return tank;
            }

        }

        return null;
    }
    
}