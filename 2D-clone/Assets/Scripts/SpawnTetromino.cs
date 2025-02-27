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
        _currentActiveTetromino.OnBlockLanded += NewTetromino;
    }
    public void StartGame()
    {
        NewTetromino();
    }
}
