using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObjectList", menuName = "ScriptableObjects/Bullet/NewScriptableObjectList")]
public class BulletSOList : ScriptableObject
{
    public BulletSO[] bulletTypeList;
}