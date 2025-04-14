using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float horizontalRotationRange = 90f;
    public float minVerticalRotation = 0f;
    public float maxVerticalRotation = 60f;

    public float horizontalSensitivity = 2f;
    public float verticalSensitivity = 2f;

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalRotationRange, horizontalRotationRange);
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

        Quaternion xRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        Quaternion yRotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        transform.localRotation = originalRotation * yRotation * xRotation;
    }
}
