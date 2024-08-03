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
        if (transform.position.y is > 7 or < -7)
        {
            if (playerManager._isDead == false)
            {
                Die();
            }
        }
    }

    // --- Die Method ---
    private void Die()
    {
        if (OnDeath != null && playerManager._isDead == false)
        {
            OnDeath();
            AudioManager.instance.Play("deathSound");
            
            playerManager._isDead = true;
            playerManager._isInputEnabled = false;
        }
    }

    // --- PickupCoin Method ---
    private void PickupCoin(int coins)
    {
        if (OnPickupCoin != null && playerManager._isDead == false)
        {
            OnPickupCoin(coins);
            AudioManager.instance.Play("coinSound");
        }
    }

    // --- AddScore Method ---
    private void AddScore(int score)
    {
        if (OnAddScore != null && playerManager._isDead == false)
        {
            OnAddScore(score);
        }
    }
    
    // --- Collision check ---
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
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
