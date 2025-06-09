using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _ammoPrefab;
    private int _score = 0;
    private int _ammoCount = 7;

    void Start()
    {
        _score = 0;
        _ammoCount = 7;
        EventManager.Instance.SetGameManager(this);
        EventManager.Instance.OnStartGame += SpawnAmmo;
        EventManager.Instance.AmmoLaunched += DecrementAmmoCount;
        EventManager.Instance.Scored += IncrementScore;
        EventManager.Instance.ShowRetry += CheckHighScore;
    }
    void OnDestroy()
    {
        EventManager.Instance.OnStartGame -= SpawnAmmo;
        EventManager.Instance.AmmoLaunched -= DecrementAmmoCount;
        EventManager.Instance.Scored -= IncrementScore;
        EventManager.Instance.ShowRetry -= CheckHighScore;

    }

    #region Actions
    /// <summary>
    /// Spawns ammo at the center of the screen in world coordinates.
    /// </summary>
    public void SpawnAmmo()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0.5f);
        Vector3 spawnWorldPos = Camera.main.ScreenToWorldPoint(screenCenter);
        GameObject ammo = Instantiate(_ammoPrefab, spawnWorldPos, Quaternion.identity);
        //Debugger.ShowText("Ammo spawned at: " + Camera.main.transform.position);
        ammo.GetComponent<SlingshotAmmo>().Spawn();
    }
    /// <summary>
    /// Increments the score by 10 points and updates the score display through the EventManager.
    /// </summary>
    public void IncrementScore()
    {
        _score += 10;
        EventManager.Instance.UpdateScore();
    }

    /// <summary>
    /// Decrements the ammo count by 1 and updates the ammo count display through the EventManager.
    /// </summary>
    public void DecrementAmmoCount()
    {
        Debugger.ShowText("Ammo count before decrement: " + _ammoCount);
        _ammoCount--;
        Debugger.AppendText("Ammo count decremented: " + _ammoCount);
        EventManager.Instance.UpdateAmmoCount(_ammoCount);
        //Debugger.ShowText("Ammo count decremented: " + _ammoCount);
    }

    /// <summary>
    /// Checks if the current score is higher than the stored high score in PlayerPrefs and updates it if necessary.
    /// </summary>
    private void CheckHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            Debugger.ShowText("New High Score: " + _score);
        }
        else
        {
            Debugger.ShowText("Current Score: " + _score + ", High Score: " + PlayerPrefs.GetInt("HighScore", 0));
        }
    }
    #endregion
    #region Getters
    /// <summary>
    /// Returns the current ammo count of the game.
    /// </summary>
    /// <returns></returns>
    public int GetAmmoCount()
    {
        return _ammoCount;
    }
    /// <summary>
    /// Returns the current score of the game.
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        return _score;
    }
    #endregion
    #region Buttons Behaviour
    /// <summary>
    /// Restarts the game by resetting the score and ammo count, and triggering the reset game event.
    /// </summary>
    public void RestartGame()
    {
        _score = 0;
        _ammoCount = 7;
        EventManager.Instance.ResetGame();
    }

    /// <summary>
    /// Reloads the current game scene, effectively restarting the game.
    /// </summary>
    public void ReloadGame()
    {
        ARPlaneSelector.SelectedPlane = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// Quits the game application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
