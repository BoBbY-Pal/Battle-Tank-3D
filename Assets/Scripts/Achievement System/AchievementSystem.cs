using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSystem : MonoSingletonGeneric<AchievementSystem>
{
    [SerializeField] private AchievementHolder achievementSoList;
    [SerializeField] private CanvasRenderer achievementPanel;
    [SerializeField] private TextMeshProUGUI achievementName;
    [SerializeField] private TextMeshProUGUI achievementInfo;
    private int currentBulletFiredAchivementLevel;
    private int currentEnemiesKilledAchievementLevel;
    
    public TextMeshProUGUI bulletCountTxt;
    public TextMeshProUGUI enemyKilledTxt;

    void Start()
    {
        currentBulletFiredAchivementLevel = 0;
        currentEnemiesKilledAchievementLevel = 0;
        // EventHandler.Instance.OnEnemyDeath += EnemyDeathCountCheck;
        bulletCountTxt = FindObjectOfType<TextMeshProUGUI>();
    }


    public void BulletsFiredAchievement(int bulletCount)
    {
        // _ = count;
        bulletCountTxt.text = "Bullet Count: " + bulletCount;

        for (int i = 0; i < achievementSoList.bulletsFiredAchievementSO.achievements.Length; i++)
        {

            if (i != currentBulletFiredAchivementLevel) continue;
            if (achievementSoList.bulletsFiredAchievementSO.achievements[i].requirement == bulletCount)
            {
                StartCoroutine(UnlockAchievement(achievementSoList.bulletsFiredAchievementSO.achievements[i].name,
                    achievementSoList.bulletsFiredAchievementSO.achievements[i].info));
                currentBulletFiredAchivementLevel = i + 1;
            }
            break;
        }
    }

    public void EnemyKilledAchievement(int enemiesKilledCount)
    {
        // _ = count;
        enemyKilledTxt.text = "Killed:- " + enemiesKilledCount;
    
        for (int i = 0; i < achievementSoList.enemiesKilledAchievementSO.achievements.Length; i++)
        {
            if (i != currentEnemiesKilledAchievementLevel) continue;
            if (achievementSoList.enemiesKilledAchievementSO.achievements[i].requirement == enemiesKilledCount)
            {
                StartCoroutine(UnlockAchievement(achievementSoList.enemiesKilledAchievementSO.achievements[i].name,
                    achievementSoList.enemiesKilledAchievementSO.achievements[i].info));
               currentEnemiesKilledAchievementLevel = i + 1;
            }
            break;
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


    // private void OnDisable() 
    // {
    //     EventHandler.Instance.OnEnemyDeath -= EnemyDeathCountCheck;
    // }

}