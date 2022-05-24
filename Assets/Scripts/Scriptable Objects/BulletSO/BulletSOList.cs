using UnityEngine;

namespace Scriptable_Objects.BulletSO
{
    [CreateAssetMenu(fileName = "BulletScriptableObjectList", menuName = "ScriptableObjects/Bullet/NewScriptableObjectList")]
    public class BulletSOList : ScriptableObject
    {
        public BulletSO[] bulletTypeList;
    }
}