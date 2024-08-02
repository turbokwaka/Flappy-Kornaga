using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    public static bool gameIsPaused = false;
    
    public void Pause()
    {
        var gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
        
        if (!gameLogic.GameIsOver)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MuteSfx()
    {
        AudioManager.instance.MuteSfx();
    }

    public void MuteMusic()
    {
        AudioManager.instance.MuteMusic();
    }
}
