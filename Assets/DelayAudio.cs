using UnityEngine;

public class DelayAudio : MonoBehaviour
{

    public AudioSource audioSource;
    public float skip = 0f;

    void Awake()
    {
        audioSource.time = skip;
        audioSource.Play();
    }
}
