using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Blinkbehavior : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.3f;

    private float fadeTimer = 0f;

    private bool startFadeOut = false;
    private bool startFadeIn = false;

    private bool isBlinkingLoop = false;

    void Start()
    {
        startFadeOut = false;
        canvasGroup.alpha = 1f;
        StartCoroutine(StartFadeInAfterDelay(0.1f));
    }

    private IEnumerator StartFadeInAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        startFadeIn = true;
    }

    public void FadeOut()
    {
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            if (!isBlinkingLoop)
                StartCoroutine(BlinkSequence());
        }
        else
        {
            startFadeOut = true;
            startFadeIn = false;
            fadeTimer = 0f;
        }
    }

    void Update()
    {
        if (startFadeIn)
        {
            fadeTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(fadeTimer / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, progress);

            if (progress >= 1f)
            {
                startFadeIn = false;
                fadeTimer = 0f;
            }
        }
        else if (startFadeOut)
        {
            fadeTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(fadeTimer / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);

            if (progress >= 1f)
            {
                startFadeOut = false;
                fadeTimer = 0f;

                if (SceneManager.GetActiveScene().buildIndex < 8)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    private IEnumerator BlinkSequence()
    {
        isBlinkingLoop = true;

        int blinkCount = 4;
        float pauseBetween = 0.4f;

        for (int i = 0; i < blinkCount; i++)
        {
            yield return StartCoroutine(FadeCanvas(0f, 1f, fadeDuration));
            yield return new WaitForSeconds(pauseBetween);

            if (i < blinkCount - 1)
            {
                yield return StartCoroutine(FadeCanvas(1f, 0f, fadeDuration));
                yield return new WaitForSeconds(pauseBetween);
            }

            fadeDuration *= 1.5f;
            pauseBetween *= 1.25f;
        }

        canvasGroup.alpha = 1f;
        isBlinkingLoop = false;
    }

    private IEnumerator FadeCanvas(float from, float to, float duration)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            canvasGroup.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
