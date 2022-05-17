using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObjectList", menuName = "ScriptableObjects/Bullet/NewScriptableObjectList")]
public class BulletScriptableObjectList : ScriptableObject
{
    public BulletScriptableObject[] bulletTypeList;
}