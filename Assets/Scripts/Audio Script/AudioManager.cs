using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource SFX;

    [Header("Audio Clips")]
    public AudioClip blocked;
    public AudioClip move;
    public AudioClip complete;
    public AudioClip fail;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            SFX.PlayOneShot(clip);
    }
}
