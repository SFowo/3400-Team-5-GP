using UnityEngine;

public class CameraRumble : MonoBehaviour
{
    public float rumbleAmount = 0.1f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector3 rumbleOffset = new Vector3(
            UnityEngine.Random.Range(-rumbleAmount, rumbleAmount),
            UnityEngine.Random.Range(-rumbleAmount, rumbleAmount),
            UnityEngine.Random.Range(-rumbleAmount, rumbleAmount)
        );

        transform.localPosition = originalPosition + rumbleOffset;
    }
}
