using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOneLiners : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        LevelLoader.instance.LoadLevel(name);
    }
    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }

    public void QuitApplication()
    {
        GameManager.instance.QuitApplication();
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
