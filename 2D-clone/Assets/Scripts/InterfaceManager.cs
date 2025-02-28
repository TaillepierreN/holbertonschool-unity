using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreNbr;
    [SerializeField] private TextMeshProUGUI _linesNbr;
    [SerializeField] private TextMeshProUGUI _levelNbr;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _HighscoreText;
    [SerializeField] private Color[] _colors;
    [SerializeField] private Image _colorBG;

    private bool _gameStarted = false;
    public float Time = 0f;

    void Start()
    {
        GameManager.Instance.OnGameStarted += GameStarted;
        GameManager.Instance.OnScoreUpdated += UpdateScore;
        GameManager.Instance.OnLevelUp += UpdateLevel;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStarted -= GameStarted;
        GameManager.Instance.OnScoreUpdated -= UpdateScore;
        GameManager.Instance.OnLevelUp -= UpdateLevel;
    }
    void Update()
    {
        if (_gameStarted)
            Time += UnityEngine.Time.deltaTime;
    }
    void LateUpdate()
    {
        if (_gameStarted)
            UpdateTimerDisplay();
    }
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(Time / 60f);
        float seconds = Time % 60f;

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateScore(int score, int lines)
    {
        _scoreNbr.text = score.ToString("D6");
        _linesNbr.text = lines.ToString("D3");
    }
    private void UpdateLevel(int level)
    {
        _levelNbr.text = level.ToString("D2");
        if( level >= 4 && _colorBG.color != _colors[1])
        {
            _colorBG.color = _colors[1];
        } else if (level >= 2 && _colorBG.color != _colors[0])
        {
            _colorBG.color = _colors[0];
        }
    }

    private void GameStarted()
    {
        _gameStarted = true;
        _HighscoreText.text = GameManager.Instance.datasave.Highscore.ToString("D6");
    }
}
