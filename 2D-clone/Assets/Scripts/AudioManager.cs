using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    [SerializeField] private AudioClip[] _bgmClips;
    [SerializeField] private AudioClip[] _sfxClips;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.OnGameStarted += PlayBGMGame;
        GameManager.Instance.OnMenuSelected += PlayBGMMenu;
        GameManager.Instance.OnRotation += PlaySFXRotation;
        GameManager.Instance.OnPlace += PlaySFXPlace;
    }
    void Update()
    {
        
    }
    private void PlayBGMGame()
    {
        BGMSource.clip = _bgmClips[1];
        BGMSource.Play();
    }
    private void PlayBGMMenu()
    {
        BGMSource.clip = _bgmClips[0];
        BGMSource.Play();
    }
    private void PlaySFXRotation()
    {
        SFXSource.clip = _sfxClips[0];
        SFXSource.Play();
    }
    private void PlaySFXPlace()
    {
        SFXSource.clip = _sfxClips[1];
        SFXSource.Play();
    }
}
