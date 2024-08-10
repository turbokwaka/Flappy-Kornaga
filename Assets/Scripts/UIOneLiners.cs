using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOneLiners : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }

    public void MainMenu()
    {
        GameManager.instance.MainMenu();
    }

    public void MuteSfx()
    {
        AudioManager.instance.MuteSFX();
    }

    public void MuteMusic()
    {
        AudioManager.instance.MuteMusic();
    }
}
