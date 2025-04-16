using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BlinkingTransitionManager : MonoBehaviour
{
    public CanvasGroup blinkCanvas;
    [Tooltip("How fast the screen fades to black")]
    public float blinkAnimationDuration = 0.15f;
    [Tooltip("Duration for each scene visibility between blinks")]
    public float[] sceneVisibilityDurations = { 0.3f, 0.2f, 0.4f, 0.3f };

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
        int blinkCount = sceneVisibilityDurations.Length;

        for (int i = 0; i < blinkCount; i++)
        {
            // Close eyes (fade to black)
            yield return Blink(true);

            // Load alternating scene
            string sceneToLoad = (i % 2 == 0) ? hospitalSceneName : currentScene;
            SceneManager.LoadScene(sceneToLoad);

            // Wait for specified duration while scene is visible
            yield return new WaitForSeconds(sceneVisibilityDurations[i]);

            // Open eyes (fade from black)
            yield return Blink(false);
        }

        // Final transition to target scene
        yield return Blink(true);
        SceneManager.LoadScene(finalScene);
        yield return new WaitForSeconds(sceneVisibilityDurations[0]); // Use first duration for final scene
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

        blinkCanvas.alpha = endAlpha; // Ensure we reach the target alpha
    }
}
