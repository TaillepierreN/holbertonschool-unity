using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private int _targetNumber = 5;
    [SerializeField] private float _spawnYOffset = 0.01f;

    private bool _hasSpawned = false;


    void Update()
    {
        if (!_hasSpawned && ARPlaneSelector.SelectedPlane != null)
        {
            _hasSpawned = true;
            SpawnTargets();
        }
    }

    private void SpawnTargets()
    {
        ARPlane plane = ARPlaneSelector.SelectedPlane;
        Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;

        Vector3 planeCenter = plane.transform.position;

        for (int i = 0; i < _targetNumber; i++)
        {
            Vector3 offset = GetRandomPositionOnPlane(plane, bounds);
            Vector3 spawnPosition = planeCenter + plane.transform.TransformVector(offset);
            spawnPosition.y += _spawnYOffset;

            GameObject target = Instantiate(_targetPrefab, spawnPosition, Quaternion.identity);

            float distance = Vector3.Distance(Camera.main.transform.position, spawnPosition);
            float scale = Mathf.Clamp(1f / distance, 0.05f, 0.3f);
            target.transform.localScale = Vector3.one * scale;

            target.GetComponent<TargetMovement>().Initialise(plane.transform);
        }
    }

    private Vector3 GetRandomPositionOnPlane(ARPlane plane, Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);

        return randomPosition;
    }
}
