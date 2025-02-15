using UnityEngine;

public class TrashGrabber : MonoBehaviour
{
    [SerializeField] private LevelManager sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        sceneManager?.IncrementScore(LevelManager.ScoreType.Collectables);
        gameObject.SetActive(false);
    }
}
