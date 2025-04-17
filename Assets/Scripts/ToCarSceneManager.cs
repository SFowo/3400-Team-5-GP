using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCarSceneManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "SubmissionStreetDrunk";
    public Blinkbehavior bb;
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
        bb.FadeOut();
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
