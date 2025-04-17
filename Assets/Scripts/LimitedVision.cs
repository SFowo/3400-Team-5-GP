using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float horizontalRotationRange = 90f;
    public float minVerticalRotation = 0f;
    public float maxVerticalRotation = 60f;

    public float horizontalSensitivity = 2f;
    public float verticalSensitivity = 2f;

    public float rotationSpeed = 360f;

    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private bool isLocked = false;
    private GameObject targetObject = null;

    void Start()
    {
        originalRotation = transform.localRotation;
        targetRotation = originalRotation;
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

            float clampedHorizontal = Mathf.Clamp(targetHorizontalAngle, -horizontalRotationRange, horizontalRotationRange);
            float clampedVertical = Mathf.Clamp(-targetVerticalAngle, minVerticalRotation, maxVerticalRotation);

            Quaternion xRotation = Quaternion.Euler(clampedVertical, 0f, 0f);
            Quaternion yRotation = Quaternion.Euler(0f, clampedHorizontal, 0f);

            targetRotation = originalRotation * yRotation * xRotation;

            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
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
