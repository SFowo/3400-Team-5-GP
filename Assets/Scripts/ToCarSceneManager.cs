using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCarSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "SubmissionStreetDrunk";
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCarTransition();
        }
    }

    private void StartCarTransition()
    {
        if (BlinkingTransitionManager.Instance != null)
        {
            BlinkingTransitionManager.Instance.StartBlinkTransition(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
