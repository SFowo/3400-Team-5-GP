using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCarSceneManager : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Submission Street");
        }    
    }
}
