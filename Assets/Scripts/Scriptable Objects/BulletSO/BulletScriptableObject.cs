using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/Bullet/NewScriptableObject")]
public class BulletScriptableObject : ScriptableObject
{
     [Header("Bullet Type")]
     [FormerlySerializedAs("BulletType")] public BulletType bulletType;
     [FormerlySerializedAs("Speed")] public int speed;
     [FormerlySerializedAs("Damage")] public int damage;
}

