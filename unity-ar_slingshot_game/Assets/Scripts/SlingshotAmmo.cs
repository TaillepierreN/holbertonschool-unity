using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SlingshotAmmo : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _arRaycastManager;

    [SerializeField] private float _resetY = -1f;
    [SerializeField] private float _force = 10f;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _arCamera;

    private Vector2 _startDragPosition;
    private bool _isDragging = false;
    private static List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touchscreen _touchscreen;


    private void Start()
    {
        _touchscreen = Touchscreen.current;
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
        if (_arCamera == null)
        {
            Debugger.ShowText("No AR Camera found, using main camera instead.");
            _arCamera = Camera.main;
        }
        if (_arRaycastManager == null)
        {
            _arRaycastManager = FindFirstObjectByType<ARRaycastManager>();
            if (_arRaycastManager == null)
            {
                Debugger.ShowText("No AR Raycast Manager found, disabling slingshot ammo.");
                enabled = false;
                return;
            }
        }
        ResetAmmo();
    }

    private void Update()
    {
        HandleToucheInput();
        CheckOutOfBonds();
        CheckPlaneHit();
    }

    private void HandleToucheInput()
    {
        if (_touchscreen == null)
            return;

        TouchControl touch = _touchscreen.primaryTouch;
        RaycastHit hit;

        if (!_isDragging)
        {
            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPos = touch.position.ReadValue();
                Ray ray = _arCamera.ScreenPointToRay(touchPos);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponentInParent<SlingshotAmmo>() == this)
                    {
                        _startDragPosition = touchPos;
                        _isDragging = true;
                        _rb.isKinematic = true;
                        //Debugger.ShowText("Drag started on: " + hit.collider.name);
                    }
                }
            }
        }
        else
        {
            Vector2 currentTouchPos = touch.position.ReadValue();

            if (touch.press.isPressed)
            {
                Vector3 dragWorldPos = _arCamera.ScreenToWorldPoint(new Vector3(currentTouchPos.x, currentTouchPos.y, 1f));
                transform.position = dragWorldPos;
            }

            if (_touchscreen.primaryTouch.press.wasReleasedThisFrame)
            {
                Vector2 dragVector = _startDragPosition - currentTouchPos;
                Vector3 forceDir = new Vector3(dragVector.x, dragVector.y, 0f).normalized;
                Vector3 worldForceDir = _arCamera.transform.TransformDirection(forceDir);

                _rb.isKinematic = false;
                _rb.linearVelocity = worldForceDir * dragVector.magnitude * _force * 0.001f;
                _isDragging = false;
                EventManager.Instance.TriggerAmmoLaunched();
            }
        }
    }

    private void CheckOutOfBonds()
    {
        if (transform.position.y < _resetY)
        {
            ResetAmmo();
        }
    }

    private void CheckPlaneHit()
    {
        if (!_isDragging && _rb.linearVelocity.y <= 0f)
        {
            Vector3 screenPos = _arCamera.WorldToScreenPoint(transform.position);
            if (_arRaycastManager.Raycast(screenPos, _hits, TrackableType.PlaneWithinPolygon))
            {
                float hitDistance = Vector3.Distance(transform.position, _hits[0].pose.position);
                if (hitDistance < 0.05f)
                {
                    ResetAmmo();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Target"))
        {
            ResetAmmo();
            Destroy(collision.gameObject);
            EventManager.Instance.TriggerScored();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            ResetAmmo();
        }
    }

    private void ResetAmmo()
    {
        if (!_arCamera)
            return;
        EventManager.Instance.CheckEndGame();
        if (EventManager.Instance.GameManager.GetAmmoCount() > 0)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, .5f);
            Vector3 worldPos = _arCamera.ScreenToWorldPoint(screenCenter);


            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.isKinematic = true;

            transform.position = worldPos;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Spawn()
    {
        ResetAmmo();
    }
}
