using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    public void Back()
    {
        SceneManager.LoadScene(SharedInfo.Instance.PreviousScene);
    }
}
