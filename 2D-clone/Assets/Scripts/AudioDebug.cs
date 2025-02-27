using UnityEngine;

public class AudioDebug : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
            Debug.Log("Audio Playing!");
        }
    }
}
