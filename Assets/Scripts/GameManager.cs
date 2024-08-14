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
    
    // --- Events ---
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        ResetLevelState();
        
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        
        PlayerCollision.OnDeath += HandlePlayerDeath;
        PlayerCollision.OnPickupCoin += AddCoins;
        PlayerCollision.OnAddScore += AddScore;
    }

    private void OnDisable()
    {
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
        OnContinue();
    }

    private IEnumerator ResumeGameWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
    }

    private void HandlePlayerDeath()
    {
        Debug.Log($" Game over?: {GameIsOver}," +
                  $" Coins: {playerCoins}," +
                  $" Score: {playerScore}," +
                  $" High Score: {playerHighestScore}");
            
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
            PlayerPrefs.Save();
        
            OnChangeHS?.Invoke(playerHighestScore);
        }
    }

    public void ResetLevelState()
    {
        GameIsOver = false;
        playerScore = 0;
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        playerHighestScore = PlayerPrefs.GetInt("PlayerHighestScore", 0);
        
        PlayerPrefs.Save();
    }

    public void QuitApplication()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
