using UnityEngine;

public class DrunkSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "SubmissionPostCrash";

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            StartCrashTransition();
        }
    }

    private void StartCrashTransition()
    {
        if (BlinkingTransitionManager.Instance != null)
        {
            BlinkingTransitionManager.Instance.StartBlinkTransition(nextSceneName);
        }
        else
        {
            Debug.LogWarning("BlinkingTransitionManager not found!");
        }
    }

}
