using System;
using System.Collections;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravityScale;

    // OnDeath EVENT
    public delegate void DeathEventHandler();
    public static event DeathEventHandler OnDeath;
    
    // OnCoinPickup EVENT
    public delegate void CoinPickupEventHandler();
    public static event CoinPickupEventHandler OnCoinPickup;
    // OnScoreAdd EVENT
    public delegate void ScoreAddEventHandler();
    public static event ScoreAddEventHandler OnScoreAddEvent;
    
    private GameLogic _gameLogic;
    private bool _canFall = false;
    private bool _isDead = false;

    void Start()
    {
        _gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

        physics.gravityScale = 0;
        
        StartCoroutine(StartFalling());
    }

    void Update()
    {
        // INPUT
        if (!_gameLogic.GameIsOver && _canFall)
        {
            if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                physics.velocity = new Vector2(physics.velocity.x, jumpStrength);
                AudioManager.instance.PlaySFX(AudioManager.instance.jumpSound);
            }

            if (transform.position.y > 7 || transform.position.y < -7)
            {
                if (_isDead == false)
                {
                    Die();
                }
            }
        }
    }
    
    void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
            AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);
            _isDead = true;
        }
    }

    private IEnumerator StartFalling()
    {
        yield return new WaitForSeconds(1.0f);
        
        physics.gravityScale = gravityScale;
        _canFall = true;
    }

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
            AudioManager.instance.PlaySFX(AudioManager.instance.coinSound);
            _gameLogic.AddCoins(1);
            Destroy(other.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PipeTrigger"))
        {
            _gameLogic.AddScore(1);
        }
    }
}
