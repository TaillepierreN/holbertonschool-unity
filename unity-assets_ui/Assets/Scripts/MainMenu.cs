using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private SharedInfo _sharedInfo;

    private void Start() {
        _sharedInfo = GameObject.Find("SharedInfo").GetComponent<SharedInfo>();
    }

    public void LevelSelect(int level)
    {
        _sharedInfo.SetPreviousScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(level);
    }

    public void Options()
    {
        _sharedInfo.SetPreviousScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        Debug.Log("Exited");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
