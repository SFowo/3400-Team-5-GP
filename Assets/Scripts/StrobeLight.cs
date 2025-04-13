using UnityEngine;

[RequireComponent(typeof(Light))]
public class StrobeLight : MonoBehaviour
{
    public float strobeSpeed = 5f;
    public float intensityMin = 25f;
    public float intensityMax = 75f;

    public float colorCycleSpeed = 0.2f;

    private Light lightSource;
    public float startHue = 0f; // Starting color

    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    void Update()
    {
        float intensity = Mathf.Lerp(intensityMin, intensityMax, (Mathf.Sin(Time.time * strobeSpeed) + 1f) / 2f);
        lightSource.intensity = intensity;

        startHue += Time.deltaTime * colorCycleSpeed;
        if (startHue > 1f) startHue -= 1f;

        lightSource.color = Color.HSVToRGB(startHue, 1f, 1f);
    }
}
