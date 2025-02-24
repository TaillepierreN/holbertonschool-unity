using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public static int GridWidth = 10;
    public static int GridHeight = 20;

    private float _previousTime;
    [SerializeField] private float _fallTime = .8f;

    // Update is called once per frame
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

        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? _fallTime / 10 : _fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            CheckValidMove(2);
            _previousTime = Time.time;
        }
    }

/// <summary>
/// Check if the move is valid
/// </summary>
/// <param name="typeOfMove">0=left, 1=right, 2=down</param>
    private void CheckValidMove(int typeOfMove)
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
                    break;
                default:
                    break;
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if( roundedX < 0 || roundedX >= GridWidth || roundedY < 0 || roundedY >= GridHeight)
            {
                return false;
            }
        }
        return true;
    }
}
