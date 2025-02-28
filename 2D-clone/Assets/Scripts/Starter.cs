using UnityEngine;
using TMPro;
using System.Collections;

public class Starter : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private SpawnTetromino _spawnTetromino;

    void Start()
    {
        Time.timeScale = 1;
        LaunchGame();
    }

    private void LaunchGame()
    {
        StartCoroutine(TimerStart());
    }

    IEnumerator TimerStart()
    {
        int time = 3;
        while (time > 0)
        {
            _timer.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        _timer.text = "GO!";
        yield return new WaitForSeconds(1);
        _canvas.SetActive(false);
        _spawnTetromino.StartGame();
    }
}
