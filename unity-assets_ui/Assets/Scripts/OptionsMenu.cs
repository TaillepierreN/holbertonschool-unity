using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private SharedInfo _sharedInfo;
    void Start()
    {
        _sharedInfo = GameObject.Find("SharedInfo").GetComponent<SharedInfo>();
    }
    public void Back()
    {
        SceneManager.LoadScene(_sharedInfo.PreviousScene);
    }
}
