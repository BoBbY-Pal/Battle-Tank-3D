using UnityEngine;
using UnityEngine.Serialization;

[ CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/NewTankScriptableObject") ]
public class TankScriptableObject : ScriptableObject
{
    [FormerlySerializedAs("TankType")] public TankType tankType;
    [FormerlySerializedAs("TankControllerType")] public TankControllerType tankControllerType;
    [FormerlySerializedAs("BulletType")] public BulletType bulletType;
    [FormerlySerializedAs("MovementSpeed")] public int movementSpeed;
    [FormerlySerializedAs("RotationRate")] public int rotationRate;
    [FormerlySerializedAs("TurretRotationRate")] public int turretRotationRate;
}

[CreateAssetMenu(fileName = "TankScriptableObjectList", menuName = "ScriptableObjects/NewTankListScriptableObject")]
public class TankScriptableObjectList : ScriptableObject
{
    public TankScriptableObject[] tanks;
}
