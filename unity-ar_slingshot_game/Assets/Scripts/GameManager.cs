using UnityEditor.SearchService;
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
    }
    void OnDestroy()
    {
        EventManager.Instance.OnStartGame -= SpawnAmmo;
        EventManager.Instance.AmmoLaunched -= DecrementAmmoCount;
        EventManager.Instance.Scored -= IncrementScore;
    }
    public void SpawnAmmo()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0.5f);
        Vector3 spawnWorldPos = Camera.main.ScreenToWorldPoint(screenCenter);
        GameObject ammo = Instantiate(_ammoPrefab, spawnWorldPos, Quaternion.identity);
        //Debugger.ShowText("Ammo spawned at: " + Camera.main.transform.position);
        ammo.GetComponent<SlingshotAmmo>().Spawn();
    }
    public void IncrementScore()
    {
        _score += 10;
        EventManager.Instance.UpdateScore();
    }
    public int GetScore()
    {
        return _score;
    }
    public void DecrementAmmoCount()
    {
        Debugger.ShowText("Ammo count before decrement: " + _ammoCount);
        _ammoCount--;
        Debugger.AppendText("Ammo count decremented: " + _ammoCount);
        EventManager.Instance.UpdateAmmoCount(_ammoCount);
        //Debugger.ShowText("Ammo count decremented: " + _ammoCount);
    }
    public int GetAmmoCount()
    {
        return _ammoCount;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
