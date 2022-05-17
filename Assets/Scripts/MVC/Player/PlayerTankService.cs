using UnityEngine;

public class PlayerTankService : MonoSingletonGeneric<PlayerTankService>
{
    public PlayerTankView tankView;
    public TankScriptableObjectList playerTankList;

    private PlayerTankController _tankController;
    private TankType _playerTankType;
    
    private void Start()
    {
        _playerTankType = TankType.Green;
        // _tankController = CreatePlayerTank(_playerTankType);
        CreateRandomTank();
    }

    private PlayerTankController CreatePlayerTank(TankType tankType)
    {
       
        // foreach (TankScriptableObject tank in playerTankList.tanks)
        foreach (var t in playerTankList.tanks)
        {
            if (t.tankType == _playerTankType) 
            {
                PlayerTankModel tankModel = new PlayerTankModel(t.movementSpeed,
                    t.health, t.rotationRate,
                    t.turretRotationRate);
                PlayerTankController tankController = new PlayerTankController(tankModel, tankView);
                return tankController;
            }
        }

        return null;
    }

    private void CreateRandomTank()
    {
        TankScriptableObject t = playerTankList.tanks[Random.Range(0, playerTankList.tanks.Length)];
        PlayerTankModel tankModel = new PlayerTankModel(t.movementSpeed,
            t.health, t.rotationRate,
            t.turretRotationRate);
        PlayerTankController tankController = new PlayerTankController(tankModel, tankView);
    }
}
