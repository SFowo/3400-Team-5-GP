using UnityEngine;
using System.Collections.Generic;

public class LookTracker : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxLookDistance = 10f;
    [SerializeField] private float lookAngle = 30f;
    [SerializeField] private bool showDebugGizmos = true;

    private LookEffectHandler currentLookedObject = null;
    private LookEffectHandler lastLookedObject = null;

    private void Update()
    {
        DetectLookedObject();
    }

    private void DetectLookedObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerCamera.transform.position, maxLookDistance);
        LookEffectHandler closestObject = null;
        float closestAngle = lookAngle;

        foreach (Collider col in hitColliders)
        {
            LookEffectHandler effectHandler = col.GetComponentInParent<LookEffectHandler>();
            if (effectHandler != null)
            {
                Vector3 toObject = (effectHandler.transform.position - playerCamera.transform.position).normalized;
                float angle = Vector3.Angle(playerCamera.transform.forward, toObject);

                if (angle < closestAngle)
                {
                    closestAngle = angle;
                    closestObject = effectHandler;
                }
            }
        }

        if (closestObject != null)
        {
            if (closestObject != currentLookedObject)
            {
                closestObject.RegisterLook();
                lastLookedObject = closestObject;
            }
            currentLookedObject = closestObject;
        }
        else
        {
            currentLookedObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugGizmos || playerCamera == null) return;

        Vector3 origin = playerCamera.transform.position;
        Vector3 forward = playerCamera.transform.forward * maxLookDistance;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, forward);

        // Draw cone boundaries
        Quaternion leftRotation = Quaternion.AngleAxis(-lookAngle, playerCamera.transform.up);
        Quaternion rightRotation = Quaternion.AngleAxis(lookAngle, playerCamera.transform.up);

        Vector3 leftDirection = leftRotation * forward;
        Vector3 rightDirection = rightRotation * forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, leftDirection);
        Gizmos.DrawRay(origin, rightDirection);
    }
}
