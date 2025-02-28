using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] Tetrominos;
    private TetrisBlock _currentActiveTetromino;

    void Start()
    {

    }

    public void NewTetromino()
    {
        if (_currentActiveTetromino != null)
            _currentActiveTetromino.OnBlockLanded -= NewTetromino;

        GameObject prefab = Tetrominos[Random.Range(0, Tetrominos.Length)];
        GameObject instantiatedMinos = Instantiate(prefab, transform.position, Quaternion.identity);
        _currentActiveTetromino = instantiatedMinos.GetComponent<TetrisBlock>();

        if (!CheckSpawnValid(instantiatedMinos))
        {
            GameManager.Instance.TriggerOnGameOver();
            Destroy(instantiatedMinos);
            return;
        }

        _currentActiveTetromino.OnBlockLanded += NewTetromino;
    }

    /// <summary>
    /// Checks if the new Tetromino can be placed at the spawn position.
    /// </summary>
private bool CheckSpawnValid(GameObject tetromino)
{
    foreach (Transform child in tetromino.transform)
    {
        int roundedX = Mathf.RoundToInt(child.position.x);
        int roundedY = Mathf.RoundToInt(child.position.y);

        if (roundedY >= TetrisBlock.GridHeight)
            continue;

        if (TetrisBlock.Grid[roundedX, roundedY] != null)
            return false;
    }
    return true;
}

    public void StartGame()
    {
        GameManager.Instance.TriggerGameStarted();
        NewTetromino();
    }
}
