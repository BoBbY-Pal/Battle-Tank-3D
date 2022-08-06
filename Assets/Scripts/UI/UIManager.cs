using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoGenericSingleton<UIManager>
    {
        [SerializeField] private CanvasRenderer achievementPanel;
        [SerializeField] private TextMeshProUGUI achievementName;
        [SerializeField] private TextMeshProUGUI achievementInfo;
        public TextMeshProUGUI bulletCountTxt;
        public TextMeshProUGUI enemiesKilledTxt;

        private void Start()
        {
            bulletCountTxt.text = "Bullet Fires: 0";
            enemiesKilledTxt.text = "Kills: 0";
        }

        public void UpdateFireCount(int bulletCount)
        {
            bulletCountTxt.text = "Bullet Fires: " + bulletCount;
        }
        
        public void UpdateKills(int killCount)
        {
            enemiesKilledTxt.text = "Kills: " + killCount;
        }
        
        public IEnumerator UnlockAchievement(string achievementName, string achievementInfo)
        {
            this.achievementName.text = achievementName;
            this.achievementInfo.text = achievementInfo;
            achievementPanel.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            achievementPanel.gameObject.SetActive(false);
        }
    }
}