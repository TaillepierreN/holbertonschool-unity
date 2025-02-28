using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool _isPaused = false;


    private void Start()
    {
        _isPaused = false;
        Time.timeScale = 1;
    }

    private void Update() {
        if (GameManager.Instance.GameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
        public void Play()
    {
        GameManager.Instance.UpdateLastSceneIndex();
        SceneManager.LoadScene("Tetris");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadTitle()
    {
        GameManager.Instance.UpdateLastSceneIndex();
        GameManager.Instance.TriggerGotToMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScoreboard()
    {
        GameManager.Instance.UpdateLastSceneIndex();
        SceneManager.LoadScene("Scoreboard");
    }

    public void BackToLastScene()
    {
        SceneManager.LoadScene(GameManager.Instance.LastSceneIndex);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(!_isPaused);
        Time.timeScale = _isPaused ? 1 : 0;
        _isPaused = !_isPaused;
    }
}
