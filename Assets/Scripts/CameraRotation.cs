using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float maxAngle; // Degree value from 0-360
    public float rotationSpeed;

    private float currentAngle = 0f;
    private bool rotatingForward = false;


    void Update()
    {
        if (maxAngle != 360) // back and forth rotation 
        {
            float step = rotationSpeed * Time.deltaTime;

            if (rotatingForward)
            {
                currentAngle += step;
                if (currentAngle >= 0)
                {
                    step = currentAngle; // makes it so it doesn't overshoot the maxAngle
                    currentAngle = 0;
                    rotatingForward = false;
                }
            }
            else
            {
                currentAngle -= step;
                if (currentAngle <= -maxAngle)
                {
                    step = currentAngle + maxAngle; // makes it so it doesn't overshoot the maxAngle
                    currentAngle = -maxAngle;
                    rotatingForward = true;
                }
            }

            //Debug.Log(rotatingForward);

            transform.RotateAround(transform.position, Vector3.up, step * (rotatingForward ? 1 : -1));
        }
        else // full circle rotation
        {
            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        
    }
}
