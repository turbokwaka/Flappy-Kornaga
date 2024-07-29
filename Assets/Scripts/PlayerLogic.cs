using System.Collections;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravityScale;

    private GameLogic _gameLogic;
    private bool _canFall = false;

    void Start()
    {
        _gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

        // Встановлення початкової гравітації на 0
        physics.gravityScale = 0;
        
        // Запуск корутини для затримки перед початком падіння
        StartCoroutine(StartFalling());
    }

    void Update()
    {
        if (!_gameLogic.GameIsOver && _canFall)
        {
            // Перевірка натискання кнопки "Jump" для ПК або торкання для мобільних пристроїв
            if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                physics.velocity = new Vector2(physics.velocity.x, jumpStrength);
                AudioManager.instance.PlaySFX(AudioManager.instance.jumpSound);
            }

            // Перевірка виходу за межі екрану
            if (transform.position.y > 7 || transform.position.y < -7)
            {
                _gameLogic.GameOver();
            }
        }
    }

    private IEnumerator StartFalling()
    {
        // Затримка на 1 секунду
        yield return new WaitForSeconds(1.0f);
        
        // Увімкнення гравітації
        physics.gravityScale = gravityScale;
        _canFall = true;
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
