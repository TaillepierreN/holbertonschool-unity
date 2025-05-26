using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    private Vector3 _direction;
    private Transform _planeTransform;

    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private float _directionChangeInterval = 2f;

    private float _timer = 0f;

    void Update()
    {
        if (_planeTransform == null)
            return;

        _timer += Time.deltaTime;
        if (_timer >= _directionChangeInterval)
        {
            SetRandomDirection();
            _timer = 0f;
        }

        Vector3 localMove = _direction * _speed * Time.deltaTime;
        Vector3 worldMove = _planeTransform.TransformDirection(localMove);
        Vector3 move = new Vector3(worldMove.x, 0, worldMove.z);
        transform.position += move;
        transform.rotation = Quaternion.LookRotation(move);
    }
    public void Initialise(Transform plane)
    {
        _planeTransform = plane;
        SetRandomDirection();
    }

    private void SetRandomDirection()
    {
        _direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }


}
