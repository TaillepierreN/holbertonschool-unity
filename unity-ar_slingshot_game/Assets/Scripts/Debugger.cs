using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{
    public static Debugger Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI debugText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        ClearText();
    }

    public static void ShowText(string message)
    {
        if (Instance != null && Instance.debugText != null)
        {
            Instance.debugText.text = message;
            Instance.debugText.gameObject.SetActive(true);
        }
    }

    public static void AppendText(string message)
    {
        if (Instance != null && Instance.debugText != null)
        {
            Instance.debugText.text += "\n" + message;
        }
    }

    public static void ClearText()
    {
        if (Instance != null && Instance.debugText != null)
        {
            Instance.debugText.text = "";
        }
    }
}
