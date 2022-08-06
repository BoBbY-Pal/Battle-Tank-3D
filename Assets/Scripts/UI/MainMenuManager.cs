using System;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        
        SFXManager.Instance.PlayMusic(SoundTypes.BackgroundMusic);
    }

    public void StartGame()
    {
        SFXManager.Instance.Play(SoundTypes.ButtonClick);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SFXManager.Instance.Play(SoundTypes.ButtonClick);
        Application.Quit();
    }
    
    
}
