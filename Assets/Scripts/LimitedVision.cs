using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float horizontalRotationRange = 90f; 
    public float sensitivity = 2f; 

    private float horizontalRotation = 0f;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;

        horizontalRotation += mouseX;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalRotationRange, horizontalRotationRange);

        Quaternion yRotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        transform.localRotation = originalRotation * yRotation;
    }
}
