using System.Collections;
using UnityEngine;

public class GrabbableBall : MonoBehaviour
{
    private bool isGrabbed = false;
    private Transform grabber;
    private Vector3 originalPosition;
    private Rigidbody rb;
    private float delay = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isGrabbed && grabber != null)
        {
            transform.position = grabber.position;
            transform.rotation = grabber.rotation;
        }
    }

    public void Grab(Transform hand)
    {
        isGrabbed = true;
        grabber = hand;
        rb.isKinematic = true;
    }

    public void Release(Vector3 throwVelocity)
    {
        isGrabbed = false;
        grabber = null;
        rb.isKinematic = false;
        rb.linearVelocity = throwVelocity;
    }
    public void StartResetPosition()
    {
        StartCoroutine(ResetPosition());
    }
    private IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localPosition = originalPosition;
    }
}
