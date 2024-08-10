using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;
    
    private void OnEnable()
    {
        PlayerCollision.OnDeath += ShowDeathScreen;
    }

    private void OnDisable()
    {
        PlayerCollision.OnDeath -= ShowDeathScreen;
    }

    public void ShowPauseMenu()
    {
        if (GameManager.instance.GameIsOver == false)
        {
            pauseMenu.SetActive(true);
            hud.SetActive(false);
        }
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        hud.SetActive(false);
    }

    public void ShowHud()
    { 
        hud.SetActive(true);
        pauseMenu.SetActive(false);
    }
}