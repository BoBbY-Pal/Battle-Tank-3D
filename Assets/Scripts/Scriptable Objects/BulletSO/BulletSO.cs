using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/Bullet/NewScriptableObject")]
public class BulletSO : ScriptableObject
{
     [Header("Prefab")] 
     public BulletView bulletView;
     
     [Header("Bullet Type")]
     public BulletType bulletType;

     [Header("Shooting Parameter")] 
     public int damage;
     public float explosionRadius;
     public float explosionForce;
     
     [Header("Durability")] 
     public float maxLifeTime;
}

