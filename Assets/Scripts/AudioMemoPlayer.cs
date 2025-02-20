using UnityEngine;

public class AudioMemoPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform progressDot;
    public Transform startPoint;
    public Transform endPoint;
    public GameObject phoneObject;

    public bool isPlaying = false;

    void Update()
    {
        if (audioSource.isPlaying)
        {
            UpdateProgress();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePlayPause();
        }
    }

    void UpdateProgress()
    {
        float progress = audioSource.time / audioSource.clip.length;
        
        progressDot.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
    }

    void TogglePlayPause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isPlaying = false;
            ToggleMeshRenderers(false);
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
            ToggleMeshRenderers(true);
        }
    }

    void ToggleMeshRenderers(bool state)
    {
        if (phoneObject != null)
        {
            MeshRenderer[] meshRenderers = phoneObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = state;
            }
        }
    }
}
