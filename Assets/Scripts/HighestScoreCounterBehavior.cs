using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighestScoreCounterBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int _playerHighestScore;

    private void Start()
    {
        _playerHighestScore = GameManager.instance.playerHighestScore;
        scoreText.text = _playerHighestScore.ToString();
    }

    void OnEnable()
    {
        GameManager.OnChangeHS += HandleChangeHS;
    }

    void OnDisable()
    {
        PlayerCollision.OnAddScore -= HandleChangeHS;
    }
    
    void HandleChangeHS(int score)
    {
        _playerHighestScore = score;
        scoreText.text = _playerHighestScore.ToString();
    }
}
