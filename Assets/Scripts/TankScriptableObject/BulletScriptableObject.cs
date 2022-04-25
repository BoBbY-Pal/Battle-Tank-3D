using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/NewBulletScriptableObject")]
public class BulletScriptableObject : ScriptableObject
{
     [FormerlySerializedAs("BulletType")] public BulletType bulletType;
     [FormerlySerializedAs("Speed")] public int speed;
     [FormerlySerializedAs("Damage")] public int damage;
}

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/NewBulletScriptableObjectList")]
public class BulletScriptableObjectList : ScriptableObject
{
    public BulletScriptableObjectList[] bulletTypeList;
}