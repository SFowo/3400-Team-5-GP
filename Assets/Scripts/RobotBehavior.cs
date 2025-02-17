using System.Runtime.CompilerServices;
using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    public float speed = 3f;
    public float distance = 10f;
    public float detectionWindow = 2f;

    private Vector3 startPos;
    private bool movingForward = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Move();
        DetectPlayer();
    }

    private void Move()
    {
        if (movingForward)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (Vector3.Distance(startPos, transform.position) >= distance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            if (Vector3.Distance(startPos, transform.position) <= 0.1f)
            {
                movingForward = true;
            }
        }
    }

    private void DetectPlayer()
    {
        float lowerBound = distance / 2 - detectionWindow / 2;
        float upperBound = distance / 2 + detectionWindow / 2;
        float distFromStart = Vector3.Distance(transform.position, startPos);

        if (distFromStart >= lowerBound && distFromStart <= upperBound)
        {
            Debug.Log("Detecting player");
        }
    }
}