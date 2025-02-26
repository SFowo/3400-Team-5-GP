using UnityEngine;

public class TriggerZoneHandler : MonoBehaviour
{
    [SerializeField] private Common.LevelManager.TriggerType triggerType;
    private Common.LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<Common.LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player entered {triggerType}");
            levelManager?.TriggerZoneEntered(other.gameObject, triggerType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player exited {triggerType}");
            levelManager?.TriggerZoneExited(other.gameObject, triggerType);
        }
    }
}