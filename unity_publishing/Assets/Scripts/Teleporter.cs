using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter destination;
    public bool teleportReady = true;
    
    public void OnTriggerEnter(Collider other)
    {
        if (!teleportReady) return;
        destination.teleportReady = false;
        other.transform.position = destination.gameObject.transform.position;
    }
    public void OnTriggerExit(Collider other)
    {
        teleportReady = true;
    }

}
