using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ARPlaneSelector : MonoBehaviour
{
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject _startButton;

    public static ARPlane SelectedPlane { get; private set; }

    private bool _isPlaneSelected = false;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    void Awake()
    {
        if (_startButton != null)
            _startButton.SetActive(false);
    }

    void Update()
    {
        if (_isPlaneSelected || Input.touchCount == 0)
            return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            TrySelectPlace(touch);
        }
    }

    private void TrySelectPlace(Touch touch)
    {
        if (_raycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon))
        {
            SelectedPlane = _planeManager.GetPlane(_hits[0].trackableId);
            if (SelectedPlane != null)
            {
                _isPlaneSelected = true;
                foreach (var plane in _planeManager.trackables)
                {
                    if (plane.trackableId != SelectedPlane.trackableId)
                    {
                        plane.gameObject.SetActive(false);
                    }
                }
                _planeManager.enabled = false;
                SelectedPlane.GetComponent<MeshRenderer>().enabled = false;

                if (_startButton != null)
                    _startButton.SetActive(true);
            }
        }
    }
}
