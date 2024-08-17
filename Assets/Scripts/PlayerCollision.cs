using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    
    // --- Events ---
    // OnDeath EVENT
    public delegate void DeathEventHandler();
    public static event DeathEventHandler OnDeath;

    // OnPickupCoin EVENT
    public delegate void CoinPickupEventHandler(int coins);
    public static event CoinPickupEventHandler OnPickupCoin;

    // OnAddScore EVENT
    public delegate void ScoreAddEventHandler(int score);
    public static event ScoreAddEventHandler OnAddScore;

    // --- Update Method ---
    private void Update()
    {
        // Check if the player goes out of screen bounds
        if (transform.position.y is > 7 or < -7 && playerManager._isInputEnabled)
        {
            Debug.Log("Player crossed game borders");
            Die();
        }    
    }

    // --- Die Method ---
    private void Die()
    {
        if (GameManager.instance.GameIsOver == false)
        {
            OnDeath?.Invoke();
            AudioManager.instance.Play("deathSound");
            
            playerManager._isInputEnabled = false;
        }
    }

    // --- PickupCoin Method ---
    private void PickupCoin(int coins)
    {
        if (GameManager.instance.GameIsOver == false)
        {
            OnPickupCoin?.Invoke(coins);
            AudioManager.instance.Play("coinSound");
        }
    }

    // --- AddScore Method ---
    private void AddScore(int score)
    {
        if (GameManager.instance.GameIsOver == false)
        {
            OnAddScore?.Invoke(score);
        }
    }
    
    // --- Collision check ---
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") && GameManager.instance.GameIsOver == false)
        {
            Debug.Log("Player touched an obstacle");
            Die();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            PickupCoin(1);
            Destroy(other.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PipeTrigger"))
        {
            AddScore(1);
        }
    }
}
