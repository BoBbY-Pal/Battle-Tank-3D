using System.Collections;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoGenericSingleton<UIManager>
    {
        [SerializeField] private CanvasRenderer achievementPanel;
        [SerializeField] private TextMeshProUGUI achievementName;
        [SerializeField] private TextMeshProUGUI achievementInfo;
        public TextMeshProUGUI bulletCountTxt;
        public TextMeshProUGUI enemiesKilledTxt;

        public GameObject pausePanel;
        private void Start()
        {
            bulletCountTxt.text = "Bullet Fires: 0";
            enemiesKilledTxt.text = "Kills: 0";
        }

        public void Pause()
        {
            SFXManager.Instance.Play(SoundTypes.ButtonClick);
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        
        public void Resume()
        {
            SFXManager.Instance.Play(SoundTypes.ButtonClick);
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        public void Restart()
        {
            SFXManager.Instance.Play(SoundTypes.ButtonClick);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        
        public void Quit()
        {
            SFXManager.Instance.Play(SoundTypes.ButtonClick);
            Application.Quit();
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