using UnityEngine;

public class EnteringLane : MonoBehaviour
{
    public GameManager gameManager;
    public TriggerType triggerType;
    public enum TriggerType { Enter, Exit }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Rigidbody ballRb = other.attachedRigidbody;
            if (triggerType == TriggerType.Enter)
            {
                gameManager.EnteringLane(ballRb);
            }
            else if (triggerType == TriggerType.Exit)
            {
                gameManager.ExitingLane();
                other.gameObject.GetComponent<GrabbableBall>().StartResetPosition();
            }

        }
	}
}
