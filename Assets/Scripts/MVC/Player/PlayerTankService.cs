using Scriptable_Objects.PlayerSO;
using UnityEngine;

public class PlayerTankService : MonoSingletonGeneric<PlayerTankService>
{
    public PlayerTankView tankViewPrefab;
    public TankScriptableObjectList playerTankList;
    public PlayerTankController player;
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
        player = new PlayerTankController(tankModel, tankViewPrefab);
    }
}
