using UnityEngine;
using UI;

public class AchievementHandler : MonoGenericSingleton<AchievementHandler>
{
    [SerializeField] private AchievementHolder achievementSoList;
    
    public void BulletsFiredAchievement(int bulletCount)
    {
        foreach (var achievementType in achievementSoList.bulletsFiredAchievementSO.achievements)
        {
            if (achievementType.requirement == bulletCount)
            {
                StartCoroutine(UIManager.Instance.UnlockAchievement(achievementType.name, achievementType.info));
            }
        }
    }

    public void EnemyKilledAchievement(int enemiesKilledCount)
    {
        foreach (var achievementType in achievementSoList.enemiesKilledAchievementSO.achievements)
        {
            if (achievementType.requirement == enemiesKilledCount)
            {
                StartCoroutine(UIManager.Instance.UnlockAchievement(achievementType.name, achievementType.info));
            }
        }
    }
    
    
}