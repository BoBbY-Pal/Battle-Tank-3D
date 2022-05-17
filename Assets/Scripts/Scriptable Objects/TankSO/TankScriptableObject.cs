using UnityEngine;
using UnityEngine.Serialization;

[ CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/Tank/NewScriptableObject") ]
public class TankScriptableObject : ScriptableObject
{
    [Header("Tank Type")]
    public TankType tankType;
    public TankControllerType tankControllerType;
    
    [Header("Health Parameter")]
    public int health;
    
    [Header("Movement Parameters")]
    public int movementSpeed;
    public int rotationRate;
    public int turretRotationRate;
    
    [Header("Shooting Parameters")]
    public BulletType bulletType;
}



