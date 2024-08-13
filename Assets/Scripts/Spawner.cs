using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public float speed;
    public float spawnRate;
    public float maximumOffset;

    private Coroutine spawnCoroutine;

    private void OnEnable()
    {
        // Subscribe to the OnPause and OnContinue events
        GameManager.OnPause += HandlePause;
        GameManager.OnContinue += HandleContinue;

        // Start the spawning coroutine
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnPause and OnContinue events to avoid memory leaks
        GameManager.OnPause -= HandlePause;
        GameManager.OnContinue -= HandleContinue;

        // Stop the coroutine if the spawner is disabled
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnPipe();

            // Randomize spawn rate and wait for the next spawn
            float _spawnRate = Random.Range(spawnRate - 0.5f, spawnRate + 0.5f);
            yield return new WaitForSeconds(_spawnRate);
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

    private void HandlePause()
    {
        // Stop the spawning coroutine when the game is paused
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private void HandleContinue()
    {
        // Restart the spawning coroutine when the game is continued
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }
}
