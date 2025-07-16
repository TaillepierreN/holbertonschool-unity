using UnityEngine;

public class EnteringLane : MonoBehaviour
{
    public WebXRMovementManager movementManager;
    public TriggerType triggerType;
    public enum TriggerType { Enter, Exit }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Rigidbody ballRb = other.attachedRigidbody;
            if (triggerType == TriggerType.Enter)
            {
                movementManager.EnterBallSteering(ballRb);
            }
            else if (triggerType == TriggerType.Exit)
            {
                movementManager.ExitBallSteering();
            }
        }
	}
}
