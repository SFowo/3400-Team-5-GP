using UnityEngine;

public class TriggerFadeOut : MonoBehaviour
{
    public Blinkbehavior bb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            bb.FadeOut();
        }
    }
}
