using System;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 RotationPoint;
    public static int GridWidth = 10;
    public static int GridHeight = 20;
    private int ScorePerLines = 100;

    private float _previousTime;
    [SerializeField] private float _fallTime = .85f;
    private static Transform[,] _grid = new Transform[GridWidth, GridHeight];
    public static Transform[,] Grid { get => _grid; }
    public event Action OnBlockLanded;

    void Start()
    {
        _fallTime = .8f - (GameManager.Instance.Level * .05f);
    }

    /// <summary>
    /// Handle inputs in update
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            CheckValidMove(0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            CheckValidMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), 90);
            if (CheckValidMove(3))
                GameManager.Instance.TriggerOnRotation();
            
        }

        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? _fallTime / 10 : _fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if(!CheckValidMove(2))
                GameManager.Instance.TriggerOnPlace();
            
            _previousTime = Time.time;
        }
    }

    /// <summary>
    /// Check if the move is valid and if not, prevent move
    /// if down is not valid, add the block to the grid,disable the script and call a new block
    /// </summary>
    /// <param name="typeOfMove">0=left, 1=right, 2=down, 3=rotate</param>
    private bool CheckValidMove(int typeOfMove)
    {
        if (!ValidMove())
        {
            switch (typeOfMove)
            {
                case 0:
                    transform.position -= new Vector3(-1, 0, 0);
                    break;
                case 1:
                    transform.position -= new Vector3(1, 0, 0);
                    break;
                case 2:
                    transform.position -= new Vector3(0, -1, 0);
                    if (transform.position.y >= GridHeight - 2)
                {
                    GameManager.Instance.TriggerOnGameOver();
                    return false;
                }
                    AddToGrid();
                    CheckForLines();
                    this.enabled = false;
                    OnBlockLanded?.Invoke();
                    break;
                case 3:
                    transform.RotateAround(transform.TransformPoint(RotationPoint), new Vector3(0, 0, 1), -90);
                    break;
                default:
                    break;
            }
            return false;
        }
        else 
        return true;
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= GridWidth || roundedY < 0 || roundedY >= GridHeight)
                return false;

            if (_grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Add the block position to the grid
    /// </summary>
    private void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            _grid[roundedX, roundedY] = children;
        }
    }

    /// <summary>
    /// Checks for complete lines in the grid and removes them.
    /// </summary>
    private void CheckForLines()
    {
        int lines = 0;
        for (int i = GridHeight - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                lines++;
                DeleteLine(i);
                RowDown(i);
            }
        }
        if (lines > 0)
        {
            int scored = lines > 1 ? ScorePerLines * (lines + lines) : ScorePerLines;
            GameManager.Instance.UpdateScore(scored, lines);
        }
    }

    /// <summary>
    /// Checks if a row has a complete line.
    /// </summary>
    /// <param name="i">The row index to check.</param>
    /// <returns>True if row has a complete line, false otherwise.</returns>
    private bool HasLine(int i)
    {
        for (int j = 0; j < GridWidth; j++)
        {
            if (_grid[j, i] == null)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Deletes a specific line from the grid.
    /// </summary>
    /// <param name="i">The row index to delete.</param>
    private void DeleteLine(int i)
    {
        ParticlesLine.Instance.StartParticule(i);
        for (int j = 0; j < GridWidth; j++)
        {
            Destroy(_grid[j, i].gameObject);
            _grid[j, i] = null;
        }
    }

    /// <summary>
    /// Moves all rows above the specified row down by one.
    /// </summary>
    /// <param name="i">The row index to start moving down from.</param>
    private void RowDown(int i)
    {
        for (int y = i; y < GridHeight; y++)
        {
            for (int j = 0; j < GridWidth; j++)
            {
                if (_grid[j, y] != null)
                {
                    _grid[j, y - 1] = _grid[j, y];
                    _grid[j, y] = null;
                    _grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
}
