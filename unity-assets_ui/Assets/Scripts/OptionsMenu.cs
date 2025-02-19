using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private SharedInfo _sharedInfo;
    void Start()
    {
        GameObject sharedInfoGO = GameObject.Find("SharedInfo");
        if (sharedInfoGO) sharedInfoGO.TryGetComponent<SharedInfo>(out _sharedInfo);
    }
    public void Back()
    {
        SceneManager.LoadScene(_sharedInfo.PreviousScene);
    }
}
