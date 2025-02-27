using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int LastSceneIndex = 0;
    public bool GameStarted = false;
    private static GameManager _instance;
    public int Score = 0;
    public int LineDone = 0;
    public int Level = 01;
    private int previousLineDone = 0;
    public event Action<int> OnLevelUp;
    public event Action<int, int> OnScoreUpdated;
    public event Action OnGameStarted;
    public event Action OnMenuSelected;
    public event Action OnRotation;
    public event Action OnPlace;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void UpdateLastSceneIndex()
    {
        LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void UpdateScore(int scoreGained, int lineDone)
    {
        Score += scoreGained;
        LineDone += lineDone;
        OnScoreUpdated?.Invoke(Score, LineDone);
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        for (int threshold = ((previousLineDone / 10) + 1) * 10; threshold <= LineDone; threshold += 10)
        {
            Level++;
            Debug.Log("Level: " + Level);
            OnLevelUp?.Invoke(Level);
        }
        previousLineDone = LineDone;
    }
    public void TriggerGameStarted()
    {
        GameStarted = true;
        OnGameStarted?.Invoke();
        ResetGame();
    }
    private void ResetGame()
    {
        Score = 0;
        LineDone = 0;
        Level = 1;
        OnScoreUpdated?.Invoke(Score, LineDone);
        OnLevelUp?.Invoke(Level);
    }
    public void TriggerGotToMenu()
    {
        OnMenuSelected?.Invoke();
    }

    public void TriggerOnRotation()
    {
        OnRotation?.Invoke();
    }
    public void TriggerOnPlace()
    {
        OnPlace?.Invoke();
    }
}
