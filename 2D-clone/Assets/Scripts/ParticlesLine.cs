using UnityEngine;

public class ParticlesLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    public static ParticlesLine Instance;

    void Start()
    {
        Instance = this;
    }


    public void StartParticule(int height)
    {
        Instance._particleSystem.gameObject.transform.position = new Vector3(0, height, 0);
        Instance._particleSystem.Play();
    }
}
