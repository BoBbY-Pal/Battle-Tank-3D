using UnityEngine;

[CreateAssetMenu(fileName = "AchievementHolder", menuName = "ScriptableObjects/Achievement/NewAchievementListSO")]
public class AchievementHolder : ScriptableObject
{
    public BulletAchievementSO bulletsFiredAchievementSO;
    public EnemyKillerAchievementSO enemiesKilledAchievementSO;
}