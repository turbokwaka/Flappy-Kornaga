using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject playerObject;
    
    [Header("-------Text Objects-------")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text highestScoreText;

    private int _playerScore;
    private int _playerHighestScore;
    private int _playerCoins;

    public bool GameIsOver = false;

    private PlayerManager playerManager;

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
            
        playerManager = playerObject.GetComponent<PlayerManager>();
        
        _playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        _playerHighestScore = PlayerPrefs.GetInt("PlayerHighestScore", 0);
        
        coinsText.text = _playerCoins.ToString();
        highestScoreText.text = _playerHighestScore.ToString();
    }
    
    void OnEnable()
    {
        PlayerCollision.OnDeath += HandlePlayerDeath;
        PlayerCollision.OnPickupCoin += AddCoins;
        PlayerCollision.OnAddScore += AddScore;
    }

    void OnDisable()
    {
        PlayerCollision.OnDeath -= HandlePlayerDeath;
    }

    void HandlePlayerDeath()
    {
        gameOverScreen.SetActive(true);
        UpdateHighestScore();
    }
    
    void AddScore(int amount)
    {
        if (!GameIsOver)
        {
            _playerScore += amount;
            scoreText.text = _playerScore.ToString();
        }
    }

    private void AddCoins(int amount)
    {
        if (!GameIsOver)
        {
            _playerCoins += amount;
            coinsText.text = _playerCoins.ToString();
            PlayerPrefs.SetInt("PlayerCoins", _playerCoins);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateHighestScore()
    {
        if (_playerScore > _playerHighestScore)
        {
            _playerHighestScore = _playerScore;
            highestScoreText.text = _playerHighestScore.ToString();

            PlayerPrefs.SetInt("PlayerHighestScore", _playerHighestScore);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}