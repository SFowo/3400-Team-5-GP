using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookEffectHandler : MonoBehaviour
{
    private enum EffectType { FadeOut, DisableMesh, ChangeColor, RotateAndFadeOut, FloatUpAndFade, EmissionGlow, ChangeScale, DeleteMaterial, FloatUpAndDestroy }

    public int LookThreshold => lookThreshold;

    [SerializeField] private int lookThreshold = 3;
    [SerializeField] private EffectType effectType;
    [SerializeField] private Color newColor = Color.red;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private Vector3 targetScale = new Vector3(0.1f, 0.1f, 0.1f);

    private bool effectTriggered;
    private int currentLookCount;

    public void RegisterLook()
    {
        if (effectTriggered) return;

        currentLookCount++;
        Debug.Log($"[{gameObject.name}]: View count [{currentLookCount}/{lookThreshold}]");

        if (currentLookCount >= lookThreshold)
        {
            ApplyEffect();
        }
    }

    private void ListObjectsAffected(Transform parent)
    {
        Debug.Log($"[{gameObject.name}]: Listing affected objects for {effectType} effect:");
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            Debug.Log($" - {child.name}");
        }
    }

    private void ApplyEffectToRenderersRecursively(Transform parent)
    {
        ListObjectsAffected(parent);

        MeshRenderer renderer = parent.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            switch (effectType)
            {
                case EffectType.DisableMesh:
                    renderer.enabled = false;
                    break;
                case EffectType.ChangeColor:
                    foreach (Material mat in renderer.materials)
                    {
                        mat.color = newColor;
                    }
                    break;
            }
        }

        foreach (Transform child in parent)
        {
            ApplyEffectToRenderersRecursively(child);
        }
    }

    public void ApplyEffect()
    {
        if (effectTriggered) return;
        effectTriggered = true;

        Debug.Log($"[{gameObject.name}]: View count [{currentLookCount}] → Effect Triggered: {effectType}");

        ListObjectsAffected(transform);

        switch (effectType)
        {
            case EffectType.FadeOut:
                StartCoroutine(FadeOut(transform));
                break;
            case EffectType.DisableMesh:
            case EffectType.ChangeColor:
                ApplyEffectToRenderersRecursively(transform);
                break;
            case EffectType.RotateAndFadeOut:
                StartCoroutine(RotateAndFadeOut(transform));
                break;
            case EffectType.FloatUpAndFade:
                StartCoroutine(FloatUpAndFade(transform));
                break;
            case EffectType.EmissionGlow:
                StartCoroutine(EmissionGlowEffect(transform));
                break;
            case EffectType.ChangeScale:
                StartCoroutine(ChangeScale(transform));
                break;
            case EffectType.DeleteMaterial:
                DeleteMaterials(transform);
                break;
            case EffectType.FloatUpAndDestroy:
                StartCoroutine(FloatUpAndDestroy(transform));
                break;
        }
    }

    private IEnumerator FadeOut(Transform parent)
    {
        float elapsedTime = 0f;
        Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                originalColors[mat] = mat.color;
            }
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            foreach (MeshRenderer renderer in renderers)
            {
                Material[] mats = renderer.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    Color originalColor = originalColors[mats[i]];
                    mats[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                }
            }

            yield return null;
        }

        ApplyEffectToRenderersRecursively(parent);
    }

    private IEnumerator RotateAndFadeOut(Transform parent)
    {
        float elapsedTime = 0f;
        Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                originalColors[mat] = mat.color;
            }
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            parent.Rotate(Vector3.up, 360f * Time.deltaTime / fadeDuration);

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            foreach (MeshRenderer renderer in renderers)
            {
                Material[] mats = renderer.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    Color originalColor = originalColors[mats[i]];
                    mats[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                }
            }

            yield return null;
        }

        ApplyEffectToRenderersRecursively(parent);
    }

    private IEnumerator FloatUpAndFade(Transform parent)
    {
        float elapsedTime = 0f;
        Vector3 startPos = parent.position;
        Vector3 endPos = startPos + Vector3.up * 2f;
        Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                originalColors[mat] = mat.color;
            }
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            parent.position = Vector3.Lerp(startPos, endPos, elapsedTime / fadeDuration);

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            foreach (MeshRenderer renderer in renderers)
            {
                Material[] mats = renderer.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    Color originalColor = originalColors[mats[i]];
                    mats[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                }
            }

            yield return null;
        }

        ApplyEffectToRenderersRecursively(parent);
    }

    private IEnumerator EmissionGlowEffect(Transform parent)
    {
        float elapsedTime = 0f;
        float duration = 1f;
        Color emissionColor = Color.yellow;

        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                mat.EnableKeyword("_EMISSION");
            }
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float intensity = Mathf.PingPong(elapsedTime * 4f, 1f);
            foreach (MeshRenderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    mat.SetColor("_EmissionColor", emissionColor * intensity);
                }
            }
            yield return null;
        }

        ApplyEffectToRenderersRecursively(parent);
    }

    private IEnumerator ChangeScale(Transform parent)
    {
        float elapsedTime = 0f;
        Vector3 originalScale = parent.localScale;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            parent.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        parent.localScale = targetScale;
    }

    private void DeleteMaterials(Transform parent)
    {
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer renderer in renderers)
        {
            Material[] mats = renderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                Destroy(mats[i]);
            }

            // Set to a default material to avoid errors
            Material defaultMat = new Material(Shader.Find("Standard"));
            defaultMat.color = Color.white;
            renderer.material = defaultMat;
        }
    }

    private IEnumerator FloatUpAndDestroy(Transform parent)
    {
        float elapsedTime = 0f;
        Vector3 startPos = parent.position;
        Vector3 endPos = startPos + Vector3.up * 3f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            parent.position = Vector3.Lerp(startPos, endPos, elapsedTime / fadeDuration);
            yield return null;
        }

        // Disable all mesh renderers
        MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        // Optionally destroy the game object after a delay
        Destroy(gameObject, 0.5f);
    }
}
