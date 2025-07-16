using UnityEngine;

public class GrabbableBall : MonoBehaviour
{
    private bool isGrabbed = false;
    private Transform grabber;
    private Rigidbody rb;

    void Start() => rb = GetComponent<Rigidbody>();

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
}
