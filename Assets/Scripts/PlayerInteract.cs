using UnityEngine;
using UnityEngine.UI;

public class RaycastObjectDisplay : MonoBehaviour
{
    public float rayDistance = 100f;
    public Text interactText;
    public EatCake eatCake; // Reference to EatCake script
    private bool CANSEEPHONE = true;
    public GameObject UIPhone;
    public SitDown sitScriptDesk;
    public SitDown sitScriptBed;

    void Update()
    {
        DisplayObjectName();
    }

    private void DisplayObjectName()
    { 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            float distanceToObject = Vector3.Distance(transform.position, hit.point);
            bool INRANGE = distanceToObject <= .5;

            if (hit.collider.gameObject.CompareTag("Phone") && CANSEEPHONE && INRANGE)
            {
                if (eatCake != null && eatCake.allCakeEaten) // Check if all cake is eaten
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "Press [E] to pick up the phone";
                    Interact(hit.collider.gameObject);
                }
                else
                {
                    interactText.gameObject.SetActive(true);
                    interactText.text = "Press [E] to eat cake";
                }
            }
            else if (sitScriptBed.SITTING)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "Press [F] to stand";
            }
            else if (hit.collider.gameObject.CompareTag("Toothbrush") && INRANGE)
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "Press [E] to pick up the toothbrush";
                Interact(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.CompareTag("Sitable") && (!sitScriptDesk.SITTING && !sitScriptBed.SITTING))
            {
                interactText.gameObject.SetActive(true);
                interactText.text = "Press [F] to sit";
            }


            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
    }

    private void Interact(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (obj.CompareTag("Phone") && eatCake != null && !eatCake.allCakeEaten)
            {
                Debug.Log("You must eat all the cake before picking up the phone!");
                return;
            }

            Destroy(obj); // Pick up (destroy) the object
            UIPhone.SetActive(true);
        }
    }
}
