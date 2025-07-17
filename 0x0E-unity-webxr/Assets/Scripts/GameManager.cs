using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WebXRMovementManager movementManager;
    public CameraSwitch cameraSwitch;
    public LaneObstacleSpawner laneObstacleSpawner;

    public void EnteringLane(Rigidbody ballRb)
    {
        movementManager.EnterBallSteering(ballRb);
        cameraSwitch.SwitchCam(true);
        laneObstacleSpawner.SpawnObstacles();
    }
    public void ExitingLane()
    {
        movementManager.ExitBallSteering();
        cameraSwitch.SwitchCam(false);
        laneObstacleSpawner.ClearObstacles();
    }
    public void ExitingLaneDelayedClear()
    {
        movementManager.ExitBallSteering();
        cameraSwitch.SwitchCam(false);
        laneObstacleSpawner.ClearObstaclesDelayed();
    }
}
