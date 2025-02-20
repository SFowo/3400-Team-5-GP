using UnityEngine;

public class SitDown : MonoBehaviour
{
    public Transform sitPoint; // Assign this as the seat position in the inspector
    public float interactionRadius = 1.5f; // Size of the area where the player can interact to sit
    public bool startSeated = false; // Determines if this is the seat the player starts in

    public static SitDown currentSeat; // Stores the seat the player is currently sitting in

    private GameObject player;
    private Camera playerCamera;
    private CharacterController playerController;
    private bool isSitting = false;
    private Quaternion originalRotation;
    private Transform originalParent;
    private Vector3 originalPlayerPosition;
    private Vector3 originalCameraLocalPosition;

    private float minYRotation;
    private float maxYRotation;
    private float cameraVerticalRotation = 0f;
    private float cameraHorizontalRotation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Ensure the player has the "Player" tag
        if (player != null)
        {
            playerCamera = Camera.main;
            playerController = player.GetComponent<CharacterController>();
        }

        // Automatically start the level with the player sitting if this seat is the starting one
        if (startSeated)
        {
            currentSeat = this; // Store the starting seat reference
            Sit(true); // Sit the player without requiring interaction
        }
    }

    void Update()
    {
        if (player == null || playerCamera == null) return;

        // Check for sitting/standing input
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isSitting)
                StandUp();
            else if (IsPlayerNear())
                Sit();
        }

        // Restrict camera rotation while sitting
        if (isSitting)
        {
            float mouseX = Input.GetAxis("Mouse X") * 2f;
            float mouseY = Input.GetAxis("Mouse Y") * 2f;

            // Track vertical rotation correctly
            cameraVerticalRotation -= mouseY;
            cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -89f, 89f);

            // Track horizontal rotation manually to avoid wrapping issues
            cameraHorizontalRotation += mouseX;
            cameraHorizontalRotation = Mathf.Clamp(cameraHorizontalRotation, minYRotation, maxYRotation);

            // Apply rotation using Quaternions
            playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
            sitPoint.rotation = Quaternion.Euler(0f, cameraHorizontalRotation, 0f);
        }
    }

    public void Sit(bool isStartingSeat = false)
    {
        if (playerController != null) playerController.enabled = false;

        originalPlayerPosition = player.transform.position;

        // Store original camera state
        originalRotation = playerCamera.transform.rotation;
        originalParent = playerCamera.transform.parent;
        originalCameraLocalPosition = playerCamera.transform.localPosition;

        // Move player to sit position
        player.transform.position = sitPoint.position;
        player.transform.rotation = sitPoint.rotation;

        // Lock camera to the chair
        playerCamera.transform.parent = sitPoint;
        playerCamera.transform.localPosition = Vector3.zero;

        // Use the chair's absolute Y rotation for consistent limits
        float chairYRotation = sitPoint.eulerAngles.y;
        minYRotation = chairYRotation - 90f;
        maxYRotation = chairYRotation + 90f;

        // Set initial horizontal rotation manually
        cameraHorizontalRotation = chairYRotation;

        // Reset manual camera rotation tracking
        cameraVerticalRotation = 0f;

        isSitting = true;
        currentSeat = this; // Store the seat the player is sitting in
    }

    public void StandUp()
    {
        if (playerController != null) playerController.enabled = false;

        // Restore player position
        player.transform.position = originalPlayerPosition;

        // Restore camera
        playerCamera.transform.parent = originalParent;
        playerCamera.transform.localPosition = originalCameraLocalPosition;
        playerCamera.transform.rotation = originalRotation;

        isSitting = false;
        currentSeat = null; // Player is no longer sitting

        if (playerController != null) playerController.enabled = true;
    }

    public bool IsPlayerCurrentlySittingHere()
    {
        return currentSeat == this;
    }

    public bool IsStartingSeat()
    {
        return startSeated;
    }

    bool IsPlayerNear()
    {
        return Vector3.Distance(player.transform.position, sitPoint.position) < interactionRadius;
    }

    // Draw Gizmos for the interaction area in the Unity Editor
    void OnDrawGizmos()
    {
        Gizmos.color = startSeated ? Color.red : Color.green; // Red for starting seat
        Gizmos.DrawWireSphere(sitPoint.position, interactionRadius);
    }
}
