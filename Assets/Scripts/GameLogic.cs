using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
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

    private PlayerLogic _playerLogic;

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
            
        _playerLogic = playerObject.GetComponent<PlayerLogic>();
        
        _playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        _playerHighestScore = PlayerPrefs.GetInt("PlayerHighestScore", 0);
        
        coinsText.text = _playerCoins.ToString();
        highestScoreText.text = _playerHighestScore.ToString();
    }
    
    void OnEnable()
    {
        PlayerCollision.OnDeath += HandlePlayerDeath;
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
    
    public void AddScore(int amount)
    {
        if (!GameIsOver)
        {
            _playerScore += amount;
            scoreText.text = _playerScore.ToString();
        }
    }

    public void AddCoins(int amount)
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
        PlayerPrefs.SetInt("PlayerCoins", _playerCoins);
        PlayerPrefs.Save();
    }
}