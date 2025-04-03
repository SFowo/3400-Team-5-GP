using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    public RawImage topLid;
    public RawImage bottomLid;
    public RenderTexture eyeLidTexture;

    public float blinkSpeed = 1.5f;
    public float blinkHoldDuration = 0.2f;

    private Vector2 topOpenPos;
    private Vector2 bottomOpenPos;
    private readonly Vector2 topClosedPos = Vector2.zero;
    private readonly Vector2 bottomClosedPos = Vector2.zero;

    private int blinkCount;

    private void Start()
    {
        // Store open positions
        topOpenPos = topLid.rectTransform.anchoredPosition;
        bottomOpenPos = bottomLid.rectTransform.anchoredPosition;

        // Start with black lids
        SetEyelidsBlack();
    }

    private void TriggerBlink()
    {
        blinkCount++;
        StartCoroutine(BlinkAnimation());
    }

    private IEnumerator BlinkAnimation()
    {
        bool isCreepyBlink = blinkCount % 3 == 0;

        if (isCreepyBlink)
        {
            SetEyelidsCreepy();
        }
        else
        {
            SetEyelidsBlack();
        }

        // Animate closing
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * blinkSpeed;
            topLid.rectTransform.anchoredPosition = Vector2.Lerp(topOpenPos, topClosedPos, t);
            bottomLid.rectTransform.anchoredPosition = Vector2.Lerp(bottomOpenPos, bottomClosedPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(blinkHoldDuration);

        // Animate opening
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * blinkSpeed;
            topLid.rectTransform.anchoredPosition = Vector2.Lerp(topClosedPos, topOpenPos, t);
            bottomLid.rectTransform.anchoredPosition = Vector2.Lerp(bottomClosedPos, bottomOpenPos, t);
            yield return null;
        }

        if (isCreepyBlink)
        {
            SetEyelidsBlack(); // Reset back to normal eyelid after blink
        }
    }

    private void SetEyelidsBlack()
    {
        topLid.texture = null;
        bottomLid.texture = null;
        topLid.color = Color.black;
        bottomLid.color = Color.black;
    }

    private void SetEyelidsCreepy()
    {
        topLid.texture = eyeLidTexture;
        bottomLid.texture = eyeLidTexture;
        topLid.color = Color.white;
        bottomLid.color = Color.white;

        // Show top and bottom halves of the eyeLid RenderTexture
        topLid.uvRect = new Rect(0f, 0.5f, 1f, 0.5f);     // Top half
        bottomLid.uvRect = new Rect(0f, 0f, 1f, 0.5f);    // Bottom half
    }

    // For testing with mouse click
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TriggerBlink();
        }
    }
}
