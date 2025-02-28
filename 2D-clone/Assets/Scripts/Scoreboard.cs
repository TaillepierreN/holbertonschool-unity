using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private int _highscore;
    [SerializeField] private TextMeshProUGUI _highscoreText;
    void Start()
    {
        _highscore = GameManager.Instance.LoadHighscore();
        _highscoreText.text = string.Format("Highest score: {0}", _highscore.ToString("D6"));
    }


}
