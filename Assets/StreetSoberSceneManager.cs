using UnityEngine;

public class StreetSoberSceneManager : MonoBehaviour
{
    public GameObject targetCar;
    public PlayerLook playerLookScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetCar != null && playerLookScript != null)
        {
            Debug.Log("Player entered trigger");
            playerLookScript.StartLookingAtTarget(targetCar);
        }
    }
}
