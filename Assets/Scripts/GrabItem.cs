using UnityEngine;

public class GrabItem : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [HideInInspector] public bool grabbed = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            grabbed = true;
            Destroy(keyPrefab);
        }
    }
}
