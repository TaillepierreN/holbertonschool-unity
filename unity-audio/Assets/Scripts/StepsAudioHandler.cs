using UnityEngine;

public class StepsAudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip grassSteps;
    [SerializeField] private AudioClip rockSteps;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Material lastSurfaceMaterial;

    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isJumping = animator.GetBool("isJumping");
        bool isFalling = animator.GetBool("isFalling");



        if (isRunning && !isJumping && !isFalling && IsGrounded())
        {
            if (!footstepSource.isPlaying)
                footstepSource.Play();
        }
        else
        {
            if (footstepSource.isPlaying)
                footstepSource.Stop();
        }
    }

    private bool IsGrounded()
    {
        return playerController.IsGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
            footstepSource.clip = rockSteps;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rock"))
            footstepSource.clip = grassSteps;
    }



}
