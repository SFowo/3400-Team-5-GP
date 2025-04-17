using System.Collections;
using UnityEngine;

public class FoyerSceneManager : MonoBehaviour
{
    [SerializeField] private GrabItem grabKey;
    [SerializeField] private GrabItem grabJacket;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource audio1;
    [SerializeField] private AudioSource audio2;
    [SerializeField] private float openDuration = 1f;
    [SerializeField] private string nextSceneName = "SubmissionToCar";

    private bool doorOpened;
    private bool playerInRange = false;

    private void Start()
    {
        if (audio1 != null)
        {
            audio1.loop = true;
            audio1.volume = 1.0f;
            audio1.Play();
        }

        if (audio2 != null)
        {
            audio2.volume = 0.2f;
            audio2.Play();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (grabKey.grabbed && !doorOpened)
            {
                doorOpened = true;
                DoorOpening();
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

    private void DoorOpening()
    {
        if (audio1 != null)
        {
            audio1.volume = 0.4f;
        }

        if (audio2 != null && !audio2.isPlaying)
        {
            audio2.volume = 0.6f;
            audio2.Play();
        }

        StartCoroutine(OpenDoorAndTransitionCoroutine());
    }

    private IEnumerator OpenDoorAndTransitionCoroutine()
    {
        Quaternion startRotation = door.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 90f, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / openDuration;
            door.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        BlinkingTransitionManager.Instance.StartBlinkTransition(nextSceneName);
    }
}