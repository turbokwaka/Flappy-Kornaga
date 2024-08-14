using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // --- Gravity fields ---
    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravityScale;

    // --- OnFly EVENT ---
    public delegate void FallEventHandler();
    public static event FallEventHandler OnFall;

    // --- private fields ---
    internal bool _isInputEnabled = false;
    private Coroutine fallCoroutine;
    private Vector2 savedVelocity;

    void Start()
    {
        physics.gravityScale = 0;
        fallCoroutine = StartCoroutine(StartFalling());
    }

    private void OnEnable()
    {
        GameManager.OnPause += HandlePause;
        GameManager.OnContinue += HandleContinue;
    }

    private void OnDisable()
    {
        GameManager.OnPause -= HandlePause;
        GameManager.OnContinue -= HandleContinue;
    }

    void Update()
    {
        // INPUT
        if (_isInputEnabled)
        {
            if (Input.GetButtonDown("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                physics.velocity = new Vector2(physics.velocity.x, jumpStrength);
                AudioManager.instance.Play("jumpSound");
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

    private void HandlePause()
    {
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null;
        }

        savedVelocity = physics.velocity;
        physics.velocity = Vector2.zero;
        physics.gravityScale = 0;
        _isInputEnabled = false;
    }

    private void HandleContinue()
    {
        if (fallCoroutine == null)
        {
            fallCoroutine = StartCoroutine(StartFalling());
        }
        
        physics.velocity = savedVelocity;
        physics.gravityScale = gravityScale;
        _isInputEnabled = true;
    }
}
