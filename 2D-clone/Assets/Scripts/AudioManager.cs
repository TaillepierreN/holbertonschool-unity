using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    [SerializeField] private AudioClip[] _bgmClips;
    [SerializeField] private AudioClip[] _sfxClips;
    public static AudioManager Instance;

    void Start()
    {
        GameManager.Instance.OnGameStarted += PlayBGMGame;
        GameManager.Instance.OnMenuSelected += PlayBGMMenu;
        GameManager.Instance.OnRotation += PlaySFXRotation;
        GameManager.Instance.OnPlace += PlaySFXPlace;
        GameManager.Instance.OnLevelUp += CheckLevel;
        GameManager.Instance.OnScoreUpdated += PlaySFXLine;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= PlayBGMGame;
        GameManager.Instance.OnMenuSelected -= PlayBGMMenu;
        GameManager.Instance.OnRotation -= PlaySFXRotation;
        GameManager.Instance.OnPlace -= PlaySFXPlace;
        GameManager.Instance.OnLevelUp -= CheckLevel;
        GameManager.Instance.OnScoreUpdated -= PlaySFXLine;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void PlayBGMGame()
    {
        SwitchMusic(1);
    }
    private void PlayBGMMenu()
    {
        SwitchMusic(0);
    }
    private void PlaySFXRotation()
    {
        SwitchSFX(0);
    }

    private void PlaySFXPlace()
    {
        if(!SFXSource.isPlaying)
            SwitchSFX(1);
    }
    private void PlaySFXLine(int score, int line)
    {
        SwitchSFX(2);
    }
    private void CheckLevel(int level)
    {
        if (level > 5)
        {
            if (BGMSource.clip != _bgmClips[3])
            {
                SwitchMusic(3);
            }
            return;
        }
        else if (level > 2)
        {
            if (BGMSource.clip != _bgmClips[2])
            {
                SwitchMusic(2);
            }
            return;
        }
    }

    private void SwitchSFX(int sfx)
    {
        SFXSource.clip = _sfxClips[sfx];
        SFXSource.Play();
    }
    private void SwitchMusic(int track)
    {
        BGMSource.loop = false;
        BGMSource.Stop();
        BGMSource.clip = _bgmClips[track];
        BGMSource.Play();
        BGMSource.loop = true;
    }
}
