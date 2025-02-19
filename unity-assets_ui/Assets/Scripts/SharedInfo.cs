using UnityEngine;

public class SharedInfo : MonoBehaviour
{
    public string PreviousScene { get; private set; } = "";
    private void Start()
    {
        if (FindObjectsByType<SharedInfo>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPreviousScene(string scene)
    {
        PreviousScene = scene;
    }
}
