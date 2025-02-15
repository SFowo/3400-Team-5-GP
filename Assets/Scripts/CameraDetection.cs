using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CameraFOV"))
        {
            Debug.Log("Player is caught!");
        }
    }
}
