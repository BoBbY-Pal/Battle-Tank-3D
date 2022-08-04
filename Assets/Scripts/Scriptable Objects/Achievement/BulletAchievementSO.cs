using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BulletsFiredAchievementSO", menuName = "ScriptableObjects/Achievement/NewBulletsFiredAchievementSO")]
public class BulletAchievementSO : ScriptableObject
{
    public AchievementType[] achievements;

    [Serializable]
    public class AchievementType
    {
        public string name;
        public string info;
        public BulletAchievementType selectAchievementType;
        public int requirement;
        
        public enum BulletAchievementType
        {
            None,
            JustGettingStarted,
            Destructor,
            Rampage,
        }
    }
}