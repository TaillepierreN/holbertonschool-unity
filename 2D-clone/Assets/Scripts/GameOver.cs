using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lineText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private GameObject gameOverPanel;

    void Start()
    {
        GameManager.Instance.OnGameOver += DisplayGameOver;
        GameManager.Instance.OnMenuSelected += HideGameOver;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= DisplayGameOver;
        GameManager.Instance.OnMenuSelected -= HideGameOver;
    }

    private void DisplayGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        int minutes = Mathf.FloorToInt(interfaceManager.Time / 60f);
        float seconds = interfaceManager.Time % 60f;

        scoreText.text = string.Format("Score : {0}", GameManager.Instance.Score.ToString("D6"));
        lineText.text = string.Format("Lines :  {0}",GameManager.Instance.LineDone.ToString("D3"));
        levelText.text = string.Format("Level :   {0}",GameManager.Instance.Level.ToString("D2"));
        timeText.text = string.Format(" Time : {0:00}:{1:00}", minutes, seconds);
    }

    private void HideGameOver()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }
}
