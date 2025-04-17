using System.Threading;
using UnityEngine;

public class TriggerAfterTime : MonoBehaviour
{
    public Blinkbehavior bb;
    public float waitTime = 10f;
    private float timer = 0f;
    private bool hasTriggered = false;

    void Update()
    {
        if (hasTriggered) return;

        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            bb.FadeOut();
            hasTriggered = true;
        }
    }
}
