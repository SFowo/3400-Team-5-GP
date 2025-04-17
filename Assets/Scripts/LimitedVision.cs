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

    private bool isLocked = false;
    private GameObject targetObject = null;

    void Start()
    {
        originalRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isLocked)
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
        else if (targetObject != null)
        {
            Vector3 directionToTarget = targetObject.transform.position - transform.position;

            float targetHorizontalAngle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
            float targetVerticalAngle = -Mathf.Atan2(directionToTarget.y, new Vector2(directionToTarget.x, directionToTarget.z).magnitude) * Mathf.Rad2Deg;

            horizontalRotation = targetHorizontalAngle;
            verticalRotation = -targetVerticalAngle;

            horizontalRotation = Mathf.Clamp(horizontalRotation, -horizontalRotationRange, horizontalRotationRange);
            verticalRotation = Mathf.Clamp(verticalRotation, minVerticalRotation, maxVerticalRotation);

            Quaternion xRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            Quaternion yRotation = Quaternion.Euler(0f, horizontalRotation, 0f);

            transform.localRotation = originalRotation * yRotation * xRotation;
        }
    }

    public void StartLookingAtTarget(GameObject target)
    {
        targetObject = target;
        isLocked = true;
    }

    public void UnlockView()
    {
        isLocked = false;
        targetObject = null;
    }
}
