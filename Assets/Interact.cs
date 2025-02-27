using System;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public enum ObjectType
    {
        Vent,
        Door,
    }

    public ObjectType type;
    public bool invertForce;
    public float forceStrength = 1f; 
    public float torqueStrength = 1f;

    private bool playerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            InteractWithObject();
        }
    }

    private void InteractWithObject()
    {
        switch (type)
        {
            case ObjectType.Vent:
                OpenVent();
                break;

            case ObjectType.Door:
                OpenDoor();
                break;
        }
    }

    private void OpenVent()
    {
        Rigidbody rd = gameObject.GetComponent<Rigidbody>();
        if (rd != null)
        {
            Vector3 forceDirection = invertForce ? Vector3.back : Vector3.forward;
            rd.constraints = RigidbodyConstraints.None;
            rd.AddForce(forceDirection * forceStrength, ForceMode.Impulse);
            rd.AddTorque(new Vector3(1, 1, 0) * torqueStrength, ForceMode.Impulse);
        }
        Debug.Log("Vent opened!");
    }

    private void OpenDoor()
    {
        Debug.Log("Door opened! Implement door logic here.");
    }
}