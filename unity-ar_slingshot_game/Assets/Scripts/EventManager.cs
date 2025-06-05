using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    public event Action OnStartGame;
    public event Action AmmoLaunched;
    public event Action Scored;
    public event Action OnScoreUpdated;
    public event Action ShowRetry;
    public event Action OnResetGame;

    public event Action<int> OnAmmoCountUpdated;

    public GameManager GameManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerStartGame()
    {
        OnStartGame?.Invoke();
    }

    public void TriggerAmmoLaunched()
    {
        AmmoLaunched?.Invoke();
    }
    public void TriggerScored()
    {
        Scored?.Invoke();
    }
    public void SetGameManager(GameManager gameManager)
    {
        GameManager = gameManager;
    }
    public void UpdateScore()
    {
        OnScoreUpdated?.Invoke();
    }
    public void UpdateAmmoCount(int ammoNbr)
    {
        OnAmmoCountUpdated?.Invoke(ammoNbr);
    }
    public void ResetGame()
    {
        OnResetGame?.Invoke();
    }
    public void CheckEndGame()
    {
        if (GameManager.GetAmmoCount() <= 0)
        {
            ShowRetry?.Invoke();
        }
    }
}
