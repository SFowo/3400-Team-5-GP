using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BlinkingTransitionManager : MonoBehaviour
{
    public CanvasGroup blinkCanvas;

    [Tooltip("How fast the screen fades to black")]
    [Range(0.05f, 0.5f)]
    public float blinkAnimationDuration = 0.1f;

    [Tooltip("Duration to show hospital scene for each blink. Array length determines number of blinks")]
    public float[] hospitalSceneVisibilityDurations = { 0.5f, 0.3f, 0.7f, 0.4f };

    [Tooltip("How long the black screen appears between scenes")]
    [Range(0.05f, 0.5f)]
    public float blackScreenDuration = 0.1f;

    public string hospitalSceneName = "SubmissionHospital1";

    public static BlinkingTransitionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartBlinkTransition(string nextScene)
    {
        StartCoroutine(BlinkSequence(nextScene));
    }

    private IEnumerator BlinkSequence(string finalScene)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int blinkCount = hospitalSceneVisibilityDurations.Length;

        for (int i = 0; i < blinkCount; i++)
        {
            // Close eyes (fade to black)
            yield return Blink(true);

            // Stay black for a moment
            yield return new WaitForSeconds(blackScreenDuration);

            // Load hospital scene while eyes are closed
            SceneManager.LoadScene(hospitalSceneName);

            // Open eyes to see hospital
            yield return Blink(false);

            // Show hospital scene for specified duration
            yield return new WaitForSeconds(hospitalSceneVisibilityDurations[i]);

            // Close eyes again
            yield return Blink(true);

            // Stay black for a moment
            yield return new WaitForSeconds(blackScreenDuration);
        }

        // Final transition to target scene
        yield return Blink(true);
        yield return new WaitForSeconds(blackScreenDuration);
        SceneManager.LoadScene(finalScene);
        yield return Blink(false);
    }

    private IEnumerator Blink(bool closing)
    {
        float time = 0;
        float startAlpha = closing ? 0 : 1;
        float endAlpha = closing ? 1 : 0;

        while (time < blinkAnimationDuration)
        {
            time += Time.deltaTime;
            blinkCanvas.alpha = Mathf.Lerp(startAlpha, endAlpha, time / blinkAnimationDuration);
            yield return null;
        }

        blinkCanvas.alpha = endAlpha;
    }
}
