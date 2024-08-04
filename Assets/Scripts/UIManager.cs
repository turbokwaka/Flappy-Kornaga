using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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