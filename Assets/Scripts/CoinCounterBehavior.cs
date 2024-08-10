using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterBehavior : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    private int _playerCoins;
    
    private void Start()
    {
        _playerCoins = GameManager.instance.playerCoins;
        coinText.text = _playerCoins.ToString();
    }
    
    void OnEnable()
    {
        PlayerCollision.OnPickupCoin += AddCoins;
    }

    void OnDisable()
    {
        PlayerCollision.OnPickupCoin -= AddCoins;
    }
    
    void AddCoins(int amount)
    {
        _playerCoins += amount;
        coinText.text = _playerCoins.ToString();
    }
}
