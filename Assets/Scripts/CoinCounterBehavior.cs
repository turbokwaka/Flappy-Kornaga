using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterBehavior : MonoBehaviour
{
    [SerializeField] private Text coinText;
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
