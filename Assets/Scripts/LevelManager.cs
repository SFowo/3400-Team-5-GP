using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
    private int collectables;
    private int paintings;
    private int detected;

    [SerializeField] private GameObject scoreUIPanel;
    [SerializeField] private Text scoreText;

    public enum ScoreType
    {
        Collectables,
        Paintings,
        Detected
    }

    public void IncrementScore(ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.Collectables:
                collectables++;
                break;
            case ScoreType.Paintings:
                paintings++;
                break;
            case ScoreType.Detected:
                detected++;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowTotalScore();
            StartCoroutine(RestartSceneAfterDelay(3f));
        }
    }

    private void ShowTotalScore()
    {
        if (scoreUIPanel != null && scoreText != null)
        {
            scoreUIPanel.SetActive(true);
            scoreText.text = $"Total Score:\n" +
                             $"Collectables: {collectables}\n" +
                             $"Paintings: {paintings}\n" +
                             $"Detected: {detected}";
        }
    }

    private System.Collections.IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
