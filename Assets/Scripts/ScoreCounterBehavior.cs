using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounterBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
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
