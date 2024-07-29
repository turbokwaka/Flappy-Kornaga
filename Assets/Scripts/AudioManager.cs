using UnityEngine;

public class AudioManager : MonoBehaviour
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

    public static AudioManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    public void MuteMusic()
    {
        backgroundMusic.mute = !backgroundMusic.mute;
    }

    public void MuteSfx()
    {
        SFX.mute = !SFX.mute;
    }
}