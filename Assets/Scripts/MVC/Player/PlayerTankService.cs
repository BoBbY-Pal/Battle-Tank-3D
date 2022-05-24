using Scriptable_Objects.TankSO;
using UnityEngine;

public class PlayerTankService : MonoSingletonGeneric<PlayerTankService>
{
    public PlayerTankView tankView;
    public TankScriptableObjectList playerTankList;

    private void Start()
    {
        CreateRandomTank();
    }

    private void CreateRandomTank()
    {
        TankScriptableObject t = playerTankList.tanks[Random.Range(0, playerTankList.tanks.Length)];
        PlayerTankModel tankModel = new PlayerTankModel(t.movementSpeed,
            t.health, t.rotationRate,
            t.turretRotationRate, t.maxLaunchForce, t.minLaunchForce, t.maxChargeTime, t.bulletType);
        PlayerTankController tankController = new PlayerTankController(tankModel, tankView);
    }
}