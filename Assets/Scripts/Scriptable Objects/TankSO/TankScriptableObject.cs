using UnityEngine;
using UnityEngine.Serialization;

[ CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/Tank/NewScriptableObject") ]
public class TankScriptableObject : ScriptableObject
{
    public TankType tankType;
    public TankControllerType tankControllerType;
    public BulletType bulletType;
    public int health;
    public int movementSpeed;
    public int rotationRate;
    public int turretRotationRate;
}



