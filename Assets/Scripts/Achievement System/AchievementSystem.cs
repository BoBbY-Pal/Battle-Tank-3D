using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSystem : MonoSingletonGeneric<AchievementSystem>
{
    [SerializeField] private AchievementHolder achievementSoList;
    [SerializeField] private CanvasRenderer achievementPanel;
    [SerializeField] private Text achievementName;
    [SerializeField] private Text achievementInfo;
    private int currentBulletFiredAchivementLevel;
    private int currentEnemiesKilledAchievementLevel;

    private int _bulletCount = 0;
    private int _deathCount = 0;
    public TextMeshProUGUI bulletCountTxt;
    public TextMeshProUGUI deathCountTxt;


    void Start()
    {
        currentBulletFiredAchivementLevel = 0;
        currentEnemiesKilledAchievementLevel = 0;
        // EventHandler.Instance.OnEnemyDeath += EnemyDeathCountCheck;
        bulletCountTxt = FindObjectOfType<TextMeshProUGUI>();
    }


    public void BulletsFiredCountCheck(int count)
    {
        _bulletCount = count;
        bulletCountTxt.text = "Bullet Count: " + _bulletCount;

        for (int i = 0; i < achievementSoList.bulletsFiredAchievementSO.achievements.Length; i++)
        {

            if (i != currentBulletFiredAchivementLevel) continue;
            if (achievementSoList.bulletsFiredAchievementSO.achievements[i].requirement == count)
            {
                StartCoroutine(UnlockAchievement(achievementSoList.bulletsFiredAchievementSO.achievements[i].name,
                    achievementSoList.bulletsFiredAchievementSO.achievements[i].info));
                currentBulletFiredAchivementLevel = i + 1;
            }
            break;
        }
    }

    // public void EnemyDeathCountCheck()
    // {
    //     Killed = EnemyTankService.Instance.tankController.tankModel.EnemiesKilled;
    //     text1.text = "Killed:- " + Killed;
    //
    //     for (int i = 0; i < achievementSOList.enemiesKilledAchievementSO.achievements.Length; i++)
    //     {
    //         if (i != currentEnemiesKilledAchievementLevel) continue;
    //         if (achievementSOList.enemiesKilledAchievementSO.achievements[i].requirement ==
    //             TankService.Instance.tankController.TankModel.EnemiesKilled)
    //         {
    //             StartCoroutine(UnlockAchievement(achievementSOList.enemiesKilledAchievementSO.achievements[i].name, achievementSOList.enemiesKilledAchievementSO.achievements[i].info));
    //            currentEnemiesKilledAchievementLevel = i + 1;
    //         }
    //         break;
    //     }
    // }
    IEnumerator UnlockAchievement(string achievementName, string achievementInfo)
    {
        this.achievementName.text = achievementName;
        this.achievementInfo.text = achievementInfo;
        achievementPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        achievementPanel.gameObject.SetActive(false);
    }


    // private void OnDisable() 
    // {
    //     EventHandler.Instance.OnEnemyDeath -= EnemyDeathCountCheck;
    // }

}