using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        
    }
}
