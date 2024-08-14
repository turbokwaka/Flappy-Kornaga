using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;
    private float _savedX, _savedY;

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

    void HandlePause()
    {
        _savedX = _x;
        _savedY = _y;

        _x = 0;
        _y = 0;
    }

    void HandleContinue()
    {
        _x = _savedX;
        _y = _savedY;
    }

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x,_y) * Time.deltaTime,_img.uvRect.size);
    }}
