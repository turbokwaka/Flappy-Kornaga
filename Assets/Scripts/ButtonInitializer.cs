using UnityEngine;
using UnityEngine.UI;

public class ButtonInitializer : MonoBehaviour
{
    [SerializeField] private Button button;

    private AudioManagerScript _audioManagerScript;

    private void Start()
    {
        _audioManagerScript = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
        if (button != null)
        {
            button.onClick.AddListener(() => _audioManagerScript.PlaySFX(_audioManagerScript.buttonSound));
        }
    }
}