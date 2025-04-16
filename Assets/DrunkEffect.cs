using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrunkEffect : MonoBehaviour
{
    public Volume volume;
    LensDistortion lensDistortion;

    public float minDistortion = -0.5f; 
    public float maxDistortion = 0.5f; 
    public float changeSpeed = 1.0f;

    float currentDistortion;

    void Start()
    {
        volume.profile.TryGet(out lensDistortion);
    }

    void Update()
    {
        if (lensDistortion != null)
        {
            float wave = Mathf.Sin(Time.time * changeSpeed);

            float t = (wave + 1f) * 0.5f;
            float distortion = Mathf.Lerp(minDistortion, maxDistortion, t);

            lensDistortion.intensity.value = distortion;
        }
    }
}
