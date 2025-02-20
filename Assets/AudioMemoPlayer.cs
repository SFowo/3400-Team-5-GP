using UnityEngine;

public class AudioMemoPlayer : MonoBehaviour
{
    public AudioSource audioSource;  // Assign your AudioSource in the Inspector
    public Transform progressDot;    // Assign the white dot object
    public Transform startPoint;     // Left-most position of the progress bar
    public Transform endPoint;       // Right-most position of the progress bar

    private bool isPlaying = false;  // Track play state

    void Update()
    {
        // Check if the audio is playing
        if (audioSource.isPlaying)
        {
            UpdateProgress();
        }

        // Pause / Resume on spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePlayPause();
        }
    }

    void UpdateProgress()
    {
        // Calculate the progress based on playback time
        float progress = audioSource.time / audioSource.clip.length;
        
        // Lerp between start and end positions
        progressDot.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
    }

    void TogglePlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            if (audioSource.time > 0) 
            {
                audioSource.UnPause();
            }
            else
            {
                audioSource.Play();
            }
            isPlaying = true;
        }
    }

}