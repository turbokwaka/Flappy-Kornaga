using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private float gravityScale = 2f;

    private AudioManagerScript _audioManagerScript;
    private GameLogicScript _gameLogic;
    void Start()
    {
        physics.gravityScale = gravityScale;
        _gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();
        _audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    void Update()
    {
        if (!_gameLogic.IsGameOver)
        {
            // Перевірка натискання кнопки "Jump" для ПК або торкання для мобільних пристроїв
            if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                physics.velocity = new Vector2(physics.velocity.x, jumpStrength);
                _audioManagerScript.PlaySFX(_audioManagerScript.jumpSound);
            }

            // Перевірка виходу за межі екрану
            if (transform.position.y > 7 || transform.position.y < -7)
            {
                _gameLogic.GameOver();
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _gameLogic.GameOver();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            _audioManagerScript.PlaySFX(_audioManagerScript.coinSound);
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