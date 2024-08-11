using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterBehavior : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int _playerScore = 0;
    
    void OnEnable()
    {
        PlayerCollision.OnAddScore += AddScore;
    }

    void OnDisable()
    {
        PlayerCollision.OnAddScore -= AddScore;
    }
    
    void AddScore(int amount)
    {
        if (GameManager.instance.GameIsOver == false)
        {
            _playerScore += amount;
            scoreText.text = _playerScore.ToString();
        }
    }
}
