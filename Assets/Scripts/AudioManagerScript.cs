using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [Header("-------Audio Sources-------")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource SFX;

    [Header("-------Audio Clips-------")] 
    public AudioClip music;
    public AudioClip buttonSound;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip deathSound;

    private static AudioManagerScript _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        backgroundMusic.clip = music;
        backgroundMusic.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        SFX.PlayOneShot(audioClip);
    }
}