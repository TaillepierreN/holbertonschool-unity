using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
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
}
