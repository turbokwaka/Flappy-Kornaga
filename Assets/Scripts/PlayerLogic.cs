using System;
using System.Collections;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    // --- Gravity fields ---
    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravityScale;
    
    // --- OnFly EVENT ---
    public delegate void FallEventHandler();
    public static event FallEventHandler OnFall;
    
    // --- private fields ---
    private GameLogic _gameLogic;
    internal bool _isInputEnabled = false;
    internal bool _isDead = false;

    void Start()
    {
        _gameLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

        physics.gravityScale = 0;
        
        StartCoroutine(StartFalling());
    }

    void Update()
    {
        // INPUT
        if (_isInputEnabled)
        {
            if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                physics.velocity = new Vector2(physics.velocity.x, jumpStrength);
                AudioManager.instance.PlaySFX(AudioManager.instance.jumpSound);
            }
        }
    }

    private IEnumerator StartFalling()
    {
        yield return new WaitForSeconds(1.0f);

        OnFall?.Invoke();

        physics.gravityScale = gravityScale;
        _isInputEnabled = true;
    }
}
