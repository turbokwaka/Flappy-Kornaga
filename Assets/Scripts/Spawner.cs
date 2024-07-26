using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public float speed;
    public float spawnRate;
    public float maximumOffset;

    private float timePassed;
    private float _spawnRate;

    void Start()
    {
        _spawnRate = Random.Range(spawnRate - 0.5f, spawnRate + 0.5f);
        SpawnPipe();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (_spawnRate < timePassed)
        {
            _spawnRate = Random.Range(spawnRate - 0.5f, spawnRate + 0.5f);
            timePassed = 0;
            SpawnPipe();
        }
    }

    private void SpawnPipe()
    {
        var offset = Random.Range(-maximumOffset, maximumOffset);
        var pipePosition = transform.position + Vector3.up * offset;
        GameObject newPipe = Instantiate(obj, pipePosition, transform.rotation);
        ObjectLogic objectLogic = newPipe.GetComponent<ObjectLogic>();
        if (objectLogic != null)
        {
            objectLogic.SetSpeed(speed);
        }
    }
}