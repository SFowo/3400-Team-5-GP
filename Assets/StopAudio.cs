using UnityEngine;
using System.Collections;

public class StopAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float stopTime = 10f;
    public float fadeDuration = 2f;

    private bool isFading = false;

    void Update()
    {
        if (!isFading && audioSource.isPlaying && audioSource.time >= stopTime)
        {
            StartCoroutine(FadeOutAndStop());
            isFading = true;
        }
    }

    private IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
    }
}
