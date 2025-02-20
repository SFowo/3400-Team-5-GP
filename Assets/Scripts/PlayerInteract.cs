using UnityEngine;
using UnityEngine.UI;

public class RaycastObjectDisplay : MonoBehaviour
{
    public float rayDistance = 100f;
    private bool CANSEEPHONE = true;
    public Text interactText;

    void Update()
    {
        DisplayObjectName();
    }

    void DisplayObjectName()
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            float distanceToObject = Vector3.Distance(transform.position, hit.point);
            bool INRANGE = distanceToObject <= .5;

            if (hit.collider.gameObject.CompareTag("Phone") && CANSEEPHONE && INRANGE)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "press [e] to pickup the phone";
            }
            else if (hit.collider.gameObject.CompareTag("Toothbrush") && INRANGE)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "press [e] to pickup the toothbrush";
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
    }
}
