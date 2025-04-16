using UnityEngine;
using DG.Tweening;

public class TurtleMovement : MonoBehaviour
{
    private float _wanderRadius = 7f;
    private float _moveDuration = 1f;
    private float _waitBetweenMoves = 2f;

    private Vector3 _startLocalPos;
    private Transform _tf;

    private void Start()
    {
        _tf = transform;
        _startLocalPos = _tf.localPosition;
        StartCoroutine(WanderRoutine());
    }

    private System.Collections.IEnumerator WanderRoutine()
    {
        while (true)
        {
            Vector2 offset = Random.insideUnitCircle * _wanderRadius;
            Vector3 localTarget = _startLocalPos + new Vector3(offset.x, 0f, offset.y);

            _tf.DOLocalMove(localTarget, _moveDuration).SetEase(Ease.InOutSine);

            Vector3 direction = (localTarget - _tf.localPosition).normalized;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                _tf.DOLocalRotateQuaternion(lookRotation, 0.5f);
            }

            yield return new WaitForSeconds(_moveDuration + _waitBetweenMoves);
        }
    }
}
