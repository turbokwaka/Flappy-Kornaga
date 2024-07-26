using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicScript : MonoBehaviour
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

    internal bool IsGameOver = false;

    private PlayerLogic _playerLogic;
    private AudioManagerScript _audioManagerScript;

    void Start()
    {
        _playerLogic = playerObject.GetComponent<PlayerLogic>();
        _audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        
        _playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        _playerHighestScore = PlayerPrefs.GetInt("PlayerHighestScore", 0);
        
        coinsText.text = _playerCoins.ToString();
        highestScoreText.text = _playerHighestScore.ToString();
    }
    
    public void AddScore(int amount)
    {
        if (!IsGameOver)
        {
            _playerScore += amount;
            scoreText.text = _playerScore.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        if (!IsGameOver)
        {
            _playerCoins += amount;
            coinsText.text = _playerCoins.ToString();
            PlayerPrefs.SetInt("PlayerCoins", _playerCoins);
        }
    }

    public void GameOver()
    {
        
        IsGameOver = true;
        gameOverScreen.SetActive(true);
        
        _audioManagerScript.PlaySFX(_audioManagerScript.deathSound);

        UpdateHighestScore();
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