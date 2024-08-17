using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Singleton instance
    public static LevelLoader instance;

    [SerializeField] private Animator transition;

    private Coroutine loadCoroutine = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(string levelName)
    {
        if (loadCoroutine == null)
        {
            loadCoroutine = StartCoroutine(LoadLevelCoroutine(levelName));
        }
    }

    private IEnumerator LoadLevelCoroutine(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1);

        SceneManager.LoadScene(levelName);

        GameManager.instance.ResetLevelState();

        transition.SetTrigger("End");

        loadCoroutine = null;
    }
}