using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour
{
    private float _speed = 6;
    private const float _deadZone = -20;

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
        if (GameManager.instance.GameIsPaused == false)
        {
            var movement = new Vector3(-_speed, 0, 0);
            transform.position += movement * Time.deltaTime;
        }
        
        if (transform.position.x < _deadZone)
        {
            Destroy(gameObject);
        }
    }

    private void HandlePause()
    {
        SetSpeed(0);
    }

    private void HandleContinue()
    {
        SetSpeed(6);
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
}