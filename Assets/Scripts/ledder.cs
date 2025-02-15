using UnityEngine;
using Common.Player;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerClimbing>(out var climbing))
        {
            climbing.StartClimbing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<PlayerClimbing>(out var climbing))
        {
            climbing.StopClimbing();
        }
    }
}