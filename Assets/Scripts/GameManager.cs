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
    public bool GameIsOver = false;
    
    // --- OnChangeHighestScore EVENT ---
    public delegate void ChangeHighestScoreEventHandler(int newHighestScore);
    public static event ChangeHighestScoreEventHandler OnChangeHS;

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

    public void RestartGame()
    {
        GameIsOver = false;
        
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
