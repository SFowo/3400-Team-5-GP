using UnityEngine;

public class MoveToDrunkScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "SubmissionStreetDrunk";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartDrunkTransition();
        }
    }

    private void StartDrunkTransition()
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
