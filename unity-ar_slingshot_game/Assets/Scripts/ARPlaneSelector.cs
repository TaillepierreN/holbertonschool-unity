using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARPlaneSelector : MonoBehaviour
{
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private Material _selectedPlaneMaterial;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _planeSelectionUI;

    public static ARPlane SelectedPlane { get; private set; }

    private bool _isPlaneSelected = false;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    void Awake()
    {
        if (_startButton != null)
            _startButton.SetActive(false);
    }
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    void Update()
    {
        if (_isPlaneSelected || Touch.activeTouches.Count == 0)
            return;

        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                TrySelectPlace(touch.screenPosition);
                break;
            }
        }
    }

    private void TrySelectPlace(Vector2 screenPosition)
    {
        if (_raycastManager.Raycast(screenPosition, _hits, TrackableType.PlaneWithinPolygon))
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

                if (SelectedPlane.TryGetComponent<MeshRenderer>(out var meshRenderer))
                {
                    meshRenderer.material = _selectedPlaneMaterial;
                }

                if (_startButton != null)
                    _startButton.SetActive(true);
                _planeSelectionUI.SetActive(false);
            }
        }
    }

}
