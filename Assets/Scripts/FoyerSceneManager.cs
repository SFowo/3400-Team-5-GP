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

    private bool doorOpened;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (grabJacket.grabbed && grabKey.grabbed && !doorOpened)
            {
                doorOpened = true;
                DoorOpening();
            }
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

        StartCoroutine(OpenDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        Quaternion startRotation = door.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -90f, 0);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / openDuration;
            door.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }
}