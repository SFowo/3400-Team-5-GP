using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float startSpeed = 5f;
    public float acceleration = 2f;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = startSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        currentSpeed += acceleration * Time.deltaTime;
    }
}