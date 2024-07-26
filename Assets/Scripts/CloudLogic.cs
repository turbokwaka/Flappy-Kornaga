using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLogic : ObjectLogic
{
    public Sprite[] sprites;
    public float transparency;
    public float scale;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        var selectedSprite = sprites[Random.Range(0, sprites.Length)];
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = selectedSprite;

        transform.localScale = Vector3.one * Random.Range(scale - 5f, scale + 5f);
        
        SetTransparency(Random.Range(transparency - 0.2f, transparency + 0.2f));
    }

    private void SetTransparency(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = Mathf.Clamp(alpha, 0f, 1f); // Ensure alpha is between 0 and 1
        _spriteRenderer.color = color;
    }}
