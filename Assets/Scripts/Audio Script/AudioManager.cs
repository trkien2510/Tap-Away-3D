using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Toggle SFXToggle;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource SFX;

    [Header("Audio Clips")]
    public AudioClip blocked;
    public AudioClip move;
    public AudioClip complete;
    public AudioClip fail;
    public AudioClip click;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int soundOn = PlayerPrefs.GetInt("SoundOn", 1);
        bool isOn = soundOn == 1;

        SFXToggle.isOn = isOn;
        SFX.mute = !isOn;

        SFXToggle.onValueChanged.AddListener(OnSoundToggleChanged);
    }

    private void Update()
    {
        if (SFXToggle == null)
        {
            SFXToggle = GameObject.Find("SFXToggle").GetComponent<Toggle>();
            int soundOn = PlayerPrefs.GetInt("SoundOn", 1);
            bool isOn = soundOn == 1;
            SFXToggle.isOn = isOn;
            SFX.mute = !isOn;
            SFXToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            SFX.PlayOneShot(clip);
    }

    private void OnSoundToggleChanged(bool isOn)
    {
        SFX.mute = !isOn;

        PlayerPrefs.SetInt("SoundOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
