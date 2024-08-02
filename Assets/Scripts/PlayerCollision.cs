using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerLogic playerLogic;
    
    // --- Events ---
    // OnDeath EVENT
    public delegate void DeathEventHandler();
    public static event DeathEventHandler OnDeath;

    // OnPickupCoin EVENT
    public delegate void CoinPickupEventHandler();
    public static event CoinPickupEventHandler OnPickupCoin;

    // OnAddScore EVENT
    public delegate void ScoreAddEventHandler();
    public static event ScoreAddEventHandler OnAddScore;

    // --- Update Method ---
    private void Update()
    {
        // Check if the player goes out of screen bounds
        if (transform.position.y is > 7 or < -7)
        {
            if (playerLogic._isDead == false)
            {
                Die();
            }
        }
    }

    // --- Die Method ---
    private void Die()
    {
        if (OnDeath != null && playerLogic._isDead == false)
        {
            OnDeath();
            AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);
            playerLogic._isDead = true;
        }
    }

    // --- PickupCoin Method ---
    private void PickupCoin()
    {
        if (OnPickupCoin != null && playerLogic._isDead == false)
        {
            OnPickupCoin();
            AudioManager.instance.PlaySFX(AudioManager.instance.coinSound);
        }
    }

    // --- AddScore Method ---
    private void AddScore()
    {
        if (OnAddScore != null && playerLogic._isDead == false)
        {
            OnAddScore();
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
            PickupCoin();
            Destroy(other.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PipeTrigger"))
        {
            AddScore();
        }
    }
}
