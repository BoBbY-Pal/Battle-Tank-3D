using UnityEngine;

namespace Scriptable_Objects.EnemySO
{
    [ CreateAssetMenu(fileName = "EnemyScriptableObjectList", menuName = "ScriptableObjects/Enemy/NewScriptableObjectList") ]
    public class EnemySOList : ScriptableObject
    {
        public EnemySO[] enemies;
    }
}