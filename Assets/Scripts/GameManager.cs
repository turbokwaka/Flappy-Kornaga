using System;
using System.Collections;
using TMPro;
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
    public bool GameIsOver;
    
    // --- OnChangeHighestScore EVENT ---
    public delegate void ChangeHighestScoreEventHandler(int newHighestScore);
    public static event ChangeHighestScoreEventHandler OnChangeHS;

    private void Awake()
    {
        if (instance == null)
        {
            // Initiate singleton
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Load player stats
            playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
            playerHighestScore = PlayerPrefs.GetInt("PlayerHighestScore", 0);
            
            // Initialize game state
            GameIsOver = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Set target frame rate and VSync settings
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void OnEnable()
    {
        // Subscribe to events
        PlayerCollision.OnDeath += HandlePlayerDeath;
        PlayerCollision.OnPickupCoin += AddCoins;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        PlayerCollision.OnDeath -= HandlePlayerDeath;
        PlayerCollision.OnPickupCoin -= AddCoins;
    }
    
    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeGameWithDelay(3f));
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
    
    private void HandleChangeHighestScore()
    {
        if (playerScore > playerHighestScore)
        {
            playerHighestScore = playerScore;
            PlayerPrefs.SetInt("PlayerHighestScore", playerHighestScore);
            
            OnChangeHS?.Invoke(playerHighestScore);
        }
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnApplicationQuit()
    {
        // Save player preferences when the application quits
        PlayerPrefs.Save();
    }
}
