using System.Security.AccessControl;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private AudioClip dialogue;
    [HideInInspector] public bool grabbed = false;

    public AudioSource keyJingle;

    private bool playerInRange = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !grabbed)
        {
            if (playerInRange)
            {
                grabbed = true;
                Destroy(keyPrefab);
                if(keyJingle != null)
                {
                    keyJingle.Play();
                }
                

            } else if(dialogue)
            {
                AudioSource.PlayClipAtPoint(dialogue, this.transform.position);
            }
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
