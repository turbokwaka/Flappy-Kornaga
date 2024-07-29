using UnityEngine;
using UnityEngine.UI;

public class ButtonInitializer : MonoBehaviour
{
    [SerializeField] private Button button;
    
    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(() => AudioManager.instance.PlaySFX(AudioManager.instance.buttonSound));
        }
    }
}