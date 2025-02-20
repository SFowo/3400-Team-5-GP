using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInEffect : MonoBehaviour
{
    public Image fadeImage;
    public float initialPause = 2f;
    public float fadeDuration = 5f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(initialPause);

        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, timer / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.clear;
    }
}
