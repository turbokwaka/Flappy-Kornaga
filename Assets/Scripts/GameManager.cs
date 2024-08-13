using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // --- Player stats ---
    public int playerScore;
    public int playerHighestScore;
    public int playerCoins;

    // --- Game state ---
    public bool GameIsOver = false;
    public bool GameIsPaused = false;
    
    // --- OnChangeHighestScore EVENT ---
    public delegate void ChangeHighestScoreEventHandler(int newHighestScore);
    public static event ChangeHighestScoreEventHandler OnChangeHS;

    public delegate void PauseEventHandler();
    public static event PauseEventHandler OnPause;
    
    public delegate void ContinueEventHandler();

    public static event ContinueEventHandler OnContinue;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnEnable()
    {
        // Load game data
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        playerHighestScore = PlayerPrefs.GetInt("PlayerHighestScore", 0);
        GameIsOver = false;
        
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        
        // Subscribe to events
        PlayerCollision.OnDeath += HandlePlayerDeath;
        PlayerCollision.OnPickupCoin += AddCoins;
        PlayerCollision.OnAddScore += AddScore;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        PlayerCollision.OnDeath -= HandlePlayerDeath;
        PlayerCollision.OnPickupCoin -= AddCoins;
        PlayerCollision.OnAddScore -= AddScore;
    }
    
    public void PauseGame()
    {
        OnPause();
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeGameWithDelay(3f));

        OnContinue();
    }

    private IEnumerator ResumeGameWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
    }

    private void HandlePlayerDeath()
    {
        GameIsOver = true;
        HandleChangeHighestScore();
    }

    private void AddCoins(int amount)
    {
        if (!GameIsOver)
        {
            playerCoins += amount;
            PlayerPrefs.SetInt("PlayerCoins", playerCoins);
        }
    }

    private void AddScore(int amount)
    {
        if (!GameIsOver)
        {
            playerScore += amount;
        }
    }
    
    private void HandleChangeHighestScore()
    {
        if (playerScore > playerHighestScore)
        {
            playerHighestScore = playerScore;
            PlayerPrefs.SetInt("PlayerHighestScore", playerHighestScore);
            
            OnChangeHS?.Invoke(playerHighestScore);
        }
    }

    public void ResetLevelState()
    {
        OnEnable();
    }

    public void QuitApplication()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
