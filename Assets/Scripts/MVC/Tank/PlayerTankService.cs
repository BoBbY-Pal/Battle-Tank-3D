using UnityEngine;

public class PlayerTankService : MonoSingletonGeneric<PlayerTankService>
{
    public PlayerTankView tankView;
    public TankScriptableObjectList playerTankList;
    public Joystick rightJoystick, leftJoystick;
    public Camera camera;

    private PlayerTankController _tankController;
    private TankType _playerTankType;
    
    private void Start()
    {
        _playerTankType = TankType.Green;
        _tankController = CreatePlayerTank(_playerTankType);
        SetPlayerTankReferences();
    }

    private void SetPlayerTankReferences()
    {
        if (_tankController != null)
        {
            _tankController.SetJoystickReference(rightJoystick, leftJoystick);
            _tankController.SetCameraReference(camera);
        }
    }

    private void FixedUpdate()
    {
        if (_tankController != null)
        {
            _tankController.FixedUpdateTankController();
        }
    }

    private PlayerTankController CreatePlayerTank(TankType tankType)
    {
        foreach (TankScriptableObject tank in playerTankList.tanks)
        {
            if (tank.tankType == _playerTankType) 
            {
                PlayerTankModel tankModel = new PlayerTankModel(playerTankList.tanks[(int) tankType].movementSpeed,
                    playerTankList.tanks[(int) tankType].health, playerTankList.tanks[(int) tankType].rotationRate,
                    playerTankList.tanks[(int) tankType].turretRotationRate);
                PlayerTankController tankController = new PlayerTankController(tankModel, tankView);
                return tankController;
            }
        }
        return null;
    }
}
