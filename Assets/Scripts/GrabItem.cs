using UnityEngine;

public class GrabItem : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [HideInInspector] public bool grabbed = false;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !grabbed)
        {
            grabbed = true;
            Destroy(keyPrefab);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
