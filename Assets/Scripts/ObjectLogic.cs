using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour
{
    private float speed = 6;
    private float _deadZone = -20;

    void Update()
    {
        var movement = new Vector3(-speed, 0, 0);
        transform.position += movement * Time.deltaTime;

        if (transform.position.x < _deadZone)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}