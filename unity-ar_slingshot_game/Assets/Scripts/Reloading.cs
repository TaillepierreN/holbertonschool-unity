using UnityEngine;
using UnityEngine.SceneManagement;

public class Reloading : MonoBehaviour
{
    public static string SceneToLoad = "ARSlingshotGame";

    void Start()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
