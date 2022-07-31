using System.Collections;
using UnityEngine;
using TMPro;

public class AchievementHandler : MonoSingletonGeneric<AchievementHandler>
{
    [SerializeField] private AchievementHolder achievementSoList;
    [SerializeField] private CanvasRenderer achievementPanel;
    [SerializeField] private TextMeshProUGUI achievementName;
    [SerializeField] private TextMeshProUGUI achievementInfo;

    public TextMeshProUGUI bulletCountTxt;
    public TextMeshProUGUI enemyKilledTxt;

    public void BulletsFiredAchievement(int bulletCount)
    {
        bulletCountTxt.text = "Bullet Count: " + bulletCount;

        foreach (var achievementType in achievementSoList.bulletsFiredAchievementSO.achievements)
        {
            if (achievementType.requirement == bulletCount)
            {
                StartCoroutine(UnlockAchievement(achievementType.name, achievementType.info));
            }
        }
    }

    public void EnemyKilledAchievement(int enemiesKilledCount)
    {
        enemyKilledTxt.text = "Killed Enemy: " + enemiesKilledCount;

        foreach (var achievementType in achievementSoList.enemiesKilledAchievementSO.achievements)
        {
            if (achievementType.requirement == enemiesKilledCount)
            {
                StartCoroutine(UnlockAchievement(achievementType.name, achievementType.info));
            }
        }
    }
    
    IEnumerator UnlockAchievement(string achievementName, string achievementInfo)
    {
        this.achievementName.text = achievementName;
        this.achievementInfo.text = achievementInfo;
        achievementPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        achievementPanel.gameObject.SetActive(false);
    }
}